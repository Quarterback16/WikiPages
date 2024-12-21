namespace WikiPages
{
    public class WikiColumnCentred : WikiColumn
    {
        public WikiColumnCentred(
               string header) : base(header)
        {
            Header = header;
            Centred = true;
        }

        public override string HeaderCode(int width) =>

            $":{new string('-', width)}:";

    }
}
