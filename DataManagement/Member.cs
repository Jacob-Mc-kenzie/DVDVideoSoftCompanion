namespace DataManagement
{
    public struct AdressT
    {
        public int number;
        public char numberappend;
        public string street;
        public string postcode;
        public string suburb;

        public AdressT(int number, string street, string postcode) : this()
        {
            this.number = number;
            this.street = street;
            this.postcode = postcode;
            this.numberappend = ' ';
            this.suburb = "";
        }

        public AdressT(int number, string street, string postcode, string suburb) : this(number, street, postcode)
        {
            this.suburb = suburb;
        }

        public AdressT(int number, char numberappend, string street, string postcode, string suburb) : this(number,street,postcode,suburb)
        {
            this.numberappend = numberappend;
        }
    }
    public class Member
    {
        private string firstNmae;
        private string surName;
        private string password;
        private AdressT adress;
        private string phonenumber;
        public string userName { get { return surName + firstNmae; } }
        private MovieCollection borrowed;

        public Member(string firstNmae, string surName, string phoneNumber, string password, AdressT adress)
        {
            this.firstNmae = firstNmae;
            this.surName = surName;
            this.phonenumber = phoneNumber;
            this.password = password;
            this.adress = adress;
            borrowed = new MovieCollection();
        }

        public bool Login(string password)
        {
            return this.password == password;
        }

        public AdressT GetAdress()
        {
            return adress;
        }
        public string GetPhoneNumber()
        {
            return phonenumber;
        }
        public void Borrow(Movie movie)
        {
            borrowed.AddDVD(movie);
            movie.Borrow();
        }

    }
}
