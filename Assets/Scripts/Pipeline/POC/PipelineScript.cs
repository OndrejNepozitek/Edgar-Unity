namespace Assets.Scripts.Pipeline.POC
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	public class PipelineScript : MonoBehaviour
	{
		public PayloadGenerator PayloadGenerator;

		public List<PipelineTask> PipelineScripts;

		public void Execute()
		{
			var payload = new Payload2();
			var payloadType = payload.GetType();

			foreach (var pipelineScript in PipelineScripts)
			{
				var type = pipelineScript.GetType();
				var foundValidInterface = false;

				foreach (var interfaceType in type.GetInterfaces())
				{
					if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IPipelineTask<>))
					{
						var desiredPayloadType = interfaceType.GetGenericArguments()[0];

						if (desiredPayloadType.IsAssignableFrom(payloadType))
						{
							interfaceType.GetMethod(nameof(IPipelineTask<object>.Process)).Invoke(pipelineScript, new object[]{ payload });

							foundValidInterface = true;
							break;
						}
					}
				}

				if (foundValidInterface == false)
				{
					throw new InvalidOperationException("Valid interface not found");
				}
			}
		}

		public void Execute2()
		{
			var payload = PayloadGenerator.InitializePayload();
			var payloadType = payload.GetType();

			var configsToTasks = AppDomain
				.CurrentDomain
				.GetAssemblies()
				.SelectMany(x => x.GetTypes())
				.Select(x => new {Type = x, Attributes = x.GetCustomAttributes(typeof(PipelineTaskForAttribute), true)})
				.Where(x => x.Attributes.Length != 0)
				.ToDictionary(x => ((PipelineTaskForAttribute) x.Attributes[0]).ConfigType, x => x.Type);

			foreach (var pipelineScript in PipelineScripts)
			{
				var type = pipelineScript.GetType();
				Type taskType;

				if (!configsToTasks.TryGetValue(type, out taskType))
				{
					throw new InvalidOperationException("Corresponding task not found");
				}

				var taskGenericType = taskType.MakeGenericType(payloadType);
				var task = Activator.CreateInstance(taskGenericType);

				var configInterfaceType = typeof(IConfigurablePipelineTask<,>).MakeGenericType(payloadType, type);
				configInterfaceType.GetProperty(nameof(IConfigurablePipelineTask<object, object>.Config)).SetValue(task, pipelineScript);
				taskGenericType.GetMethod(nameof(IPipelineTask<object>.Process)).Invoke(task, new object[] { payload });
			}
		}
	}
}