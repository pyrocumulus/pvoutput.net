namespace PVOutput.Net.Objects.Systems
{
	public struct ExtendedDataElement
	{
		public string Label;
		public string Unit;

		public ExtendedDataElement(string label, string unit)
		{
			Label = label;
			Unit = unit;
		}
	}
}
