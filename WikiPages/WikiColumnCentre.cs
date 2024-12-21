namespace WikiPages
{
    public class WikiColumnCentre : WikiColumn
    {
        public WikiColumnCentre(
               string header) : base(header)
        {
            Header = header;
            RightJustify = false;
        }
        public override string HeaderCode(int width) => 

            $":{new string('-', width)}:";
    }
}
