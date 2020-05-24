namespace DataManagement
{
    /// <summary>
    /// A Adress structure, While initially used, dure to some conversion headaches now Un-used.
    /// </summary>
    public struct AdressT
    {
        public int number;
        public string numberappend;
        public string street;
        public string postcode;
        public string suburb;

        public AdressT(int number, string street, string postcode) : this()
        {
            this.number = number;
            this.street = street;
            this.postcode = postcode;
            this.numberappend = "";
            this.suburb = "";
        }

        public AdressT(int number, string street, string postcode, string suburb) : this(number, street, postcode)
        {
            this.suburb = suburb;
        }

        public AdressT(int number, string numberappend, string street, string postcode, string suburb) : this(number,street,postcode,suburb)
        {
            this.numberappend = numberappend;
        }
    }
    /// <summary>
    /// A simple Member class.
    /// </summary>
    public class Member
    {
        private string firstNmae;
        private string surName;
        private string password;
        private string adress;
        private string phonenumber;
        /// <summary>
        /// Gets the username By concatonating the first and sur-names of the member.
        /// </summary>
        public string userName { get { return surName + firstNmae; } }
        /// <summary>
        /// A collection of the currently borrowed films.
        /// </summary>
        public MovieCollection borrowed;
        /// <summary>
        /// Creates a new member with no borrowed films.
        /// </summary>
        /// <param name="firstNmae">First name</param>
        /// <param name="surName">Sur-Name</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="password">the 4 didget numeric password</param>
        /// <param name="adress">The full address</param>
        public Member(string firstNmae, string surName, string phoneNumber, string password, string adress)
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

        public string GetAdress()
        {
            return adress;
        }
        public string GetPhoneNumber()
        {
            return phonenumber;
        }
        /// <summary>
        /// Adds the film to the users borrowed.
        /// Verification is completed at a higher level.
        /// </summary>
        /// <param name="movie">The film to borrow.</param>
        public void Borrow(Movie movie)
        {
            movie.Borrow();
            borrowed.AddDVD(movie);
        }

    }
}
