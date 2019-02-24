namespace Assets.ProceduralLevelGenerator.Scripts.Pipeline
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class PipelineRunner
	{
		/// <summary>
		/// Runs given pipeline items with a given payload.
		/// </summary>
		/// <param name="pipelineItems"></param>
		/// <param name="payload"></param>
		public void Run(IEnumerable<PipelineItem> pipelineItems, object payload)
		{
			var payloadType = payload.GetType();

			var pipelineTaskTypes = GetAllTypesImplementingOpenGenericType(typeof(IPipelineTask<>)).ToList();
			var configurablePipelineTaskTypes = GetAllTypesImplementingOpenGenericType(typeof(IConfigurablePipelineTask<,>)).ToList();

			foreach (var pipelineItem in pipelineItems)
			{
				if (pipelineItem == null)
					continue;

				var type = pipelineItem.GetType();

				// Check if the class implements IPipelineTask<> interface
				if (pipelineTaskTypes.Contains(type))
				{
					// Find all implemented IPipelineTask<TPayload> interfaces
					var relevantInterfaces = type
						.GetInterfaces()
						.Where(x => x.IsGenericType)
						.Where(x => x.GetGenericTypeDefinition() == typeof(IPipelineTask<>))
						.ToList();

					if (relevantInterfaces.Count > 1)
					{
						throw new ArgumentException($"{pipelineItem.name} - Each pipeline item must implement the IPipelineTask<> at most once");
					}

					var actualInterfaceType = relevantInterfaces[0];
					var actualPayloadType = actualInterfaceType.GetGenericArguments()[0];

					if (!actualPayloadType.IsAssignableFrom(payloadType))
					{
						throw new ArgumentException($"{pipelineItem.name} - Payload type {payloadType.Name} cannot be assigned to {actualPayloadType.Name}");
					}

					// Do pipelineScript.Payload = payload;
					actualInterfaceType
						.GetProperty(nameof(IPipelineTask<object>.Payload))
						.SetValue(pipelineItem, payload);

					// Do pipelineScript.Process();
					actualInterfaceType
						.GetMethod(nameof(IPipelineTask<object>.Process))
						.Invoke(pipelineItem, new object[0]);

				}
				else if (pipelineItem is PipelineConfig)
				{
					var relevantClasses = new List<Tuple<Type, Type>>();

					foreach (var configurablePipelineTaskType in configurablePipelineTaskTypes)
					{
						var relevantInterfaces = configurablePipelineTaskType
							.GetInterfaces()
							.Where(y => y.IsGenericType)
							.Where(y => y.GetGenericTypeDefinition() == typeof(IConfigurablePipelineTask<,>))
							.Where(y => y.GetGenericArguments().Length == 2 && y.GetGenericArguments()[1] == type)
							.ToList();

						if (relevantInterfaces.Count > 1)
						{
							throw new ArgumentException($"{configurablePipelineTaskType.Name} - Each pipeline item must implement the IConfigurablePipelineTask<,> at most once");
						}

						if (relevantInterfaces.Count == 1)
						{
							relevantClasses.Add(Tuple.Create(configurablePipelineTaskType, relevantInterfaces[0]));
						}
					}

					if (relevantClasses.Count == 0)
					{
						throw new ArgumentException($"{pipelineItem.name} - There is no pipeline task that can handle {pipelineItem.GetType().Name}");
					}

					if (relevantClasses.Count > 1)
					{
						throw new ArgumentException($"{pipelineItem.name} - There must not be more than 1 class handling {pipelineItem.GetType().Name}");
					}

					var actualPayloadType = relevantClasses[0].Item2.GenericTypeArguments[0];
					var actualTaskType = relevantClasses[0].Item1;

					if (actualPayloadType.IsGenericParameter)
					{
						try
						{
							actualTaskType = relevantClasses[0].Item1.MakeGenericType(payloadType);
						}
						catch (ArgumentException)
						{
							throw new ArgumentException($"{pipelineItem.name} - Payload of type {payloadType.Name} cannot be used in a task of type {actualTaskType.Name}");
						}
					}

					var taskInstance = Activator.CreateInstance(actualTaskType);

					// Do taskInstance.Payload = payload;
					actualTaskType
						.GetProperty(nameof(IConfigurablePipelineTask<object, PipelineConfig>.Payload))
						.SetValue(taskInstance, payload);

					// Do taskInstance.Config = pipelineScript;
					actualTaskType
						.GetProperty(nameof(IConfigurablePipelineTask<object, PipelineConfig>.Config))
						.SetValue(taskInstance, pipelineItem);

					// Do taskInstance.Process();
					actualTaskType
						.GetMethod(nameof(IConfigurablePipelineTask<object, PipelineConfig>.Process))
						.Invoke(taskInstance, new object[0]);
				}
				else
				{
					throw new InvalidOperationException();
				}
			}
		}

		private static IEnumerable<Type> GetAllTypesImplementingOpenGenericType(Type openGenericType)
		{
			return from x in AppDomain
					.CurrentDomain
					.GetAssemblies().SelectMany(x => x.GetTypes())
				   from z in x.GetInterfaces()
				   let y = x.BaseType
				   where
					   (y != null && y.IsGenericType &&
						openGenericType.IsAssignableFrom(y.GetGenericTypeDefinition())) ||
					   (z.IsGenericType &&
						openGenericType.IsAssignableFrom(z.GetGenericTypeDefinition()))
				   select x;
		}
	}
}