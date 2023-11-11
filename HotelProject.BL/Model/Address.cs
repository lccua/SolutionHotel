using HotelProject.BL.Exceptions.Model;

namespace HotelProject.BL.Model
{
    public class Address
    {
        public Address(string municipality, string zipCode, string houseNumber, string street)
        {
            _municipality = municipality;
            _zipCode = zipCode;
            _houseNumber = houseNumber;
            _street = street;
        }

        public Address(string addressLine)
        {
            // Split the address line into parts using '|' as a separator
            string[] parts = addressLine.Split(new char[] { '|' });

            // Assign values to the private fields
            _houseNumber = parts[3];
            _street = parts[1];
            _municipality = parts[0];
            _zipCode = parts[2];
        }

        //------------------------------------------------------------------

        // ToString method for generating a formatted string representation
        public override string ToString()
        {
            return $"({Municipality} [{ZipCode}] - {Street} - {HouseNumber})";
        }

        // ToAddressLine method for generating a formatted address line
        public string ToAddressLine()
        {
            return $"{Municipality}|{ZipCode}|{Street}|{HouseNumber}";
        }

        //------------------------------------------------------------------

        // Municipality property
        private string _municipality;
        public string Municipality
        {
            get { return _municipality; }
            set { ValidateMunicipality(value); _municipality = value; }
        }

        // Validation for the Municipality property
        private void ValidateMunicipality(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new AddressException("Municipality is empty");
            }
        }

        //------------------------------------------------------------------

        // ZipCode property
        private string _zipCode;
        public string ZipCode
        {
            get { return _zipCode; }
            set { ValidateZipCode(value); _zipCode = value; }
        }

        // Validation for the ZipCode property
        private void ValidateZipCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new AddressException("ZipCode is empty");
            }
        }

        //------------------------------------------------------------------

        // HouseNumber property
        private string _houseNumber;
        public string HouseNumber
        {
            get { return _houseNumber; }
            set { ValidateHouseNumber(value); _houseNumber = value; }
        }

        // Validation for the HouseNumber property
        private void ValidateHouseNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new AddressException("HouseNumber is empty");
            }
        }

        //------------------------------------------------------------------

        // Street property
        private string _street;
        public string Street
        {
            get { return _street; }
            set { ValidateStreet(value); _street = value; }
        }

        // Validation for the Street property
        private void ValidateStreet(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new AddressException("Street is empty");
            }
        }

        //------------------------------------------------------------------

    }
}
