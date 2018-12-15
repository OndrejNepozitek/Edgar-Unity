namespace Assets.Scripts.Pipeline
{
	using System;

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class PipelineTaskForAttribute : Attribute
	{
		public Type ConfigType { get; set; }

		public PipelineTaskForAttribute(Type configType)
		{
			ConfigType = configType;
		}
	}
}