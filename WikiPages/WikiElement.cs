namespace WikiPages
{
	public class WikiElement
	{
		public string Type { get; set; }
		public string Value { get; set; }  // this perhaps should be a StringBuilder
		public virtual void Render()
		{
		}
	}
}
