namespace DyeDurhamSorter
{
    public class PersonName
    {
        public string FullName { get; set; }
        public string SurName { get; set; }
        public string GivenName1 { get; set; } 
        public string? GivenName2 { get; set; }
        public string? GivenName3 { get; set; }
        public string GivenName { get; set; }

        public PersonName(string surName, string givenName1)
        {
            SurName = surName;
            GivenName1 = givenName1;

            GivenName = givenName1;
            FullName = GivenName + " " + surName;
        }

        public PersonName(string surName, string givenName1, string? givenName2)
        {
            SurName = surName;
            GivenName1 = givenName1;
            GivenName2 = givenName2;

            GivenName = givenName1 + (givenName2 != null ? " " + givenName2 : "");
            FullName = GivenName + " " + surName;
        }

        public PersonName(string surName, string givenName1, string? givenName2, string? givenName3)
        {
            SurName = surName;
            GivenName1 = givenName1;
            GivenName2 = givenName2;
            GivenName3 = givenName3;

            GivenName = givenName1 + (givenName2 != null ? " " + givenName2 : "")
                        + (givenName3 != null ? " " + givenName3 : "");
            FullName = GivenName + " " + surName; 
        }
    }
}
