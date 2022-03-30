namespace LegacyApp
{
    public class ClientRepository
    {
        public ClientRepository()
        {
        }

        internal Client GetById(int clientId)
        {
            //Fetching the data...
            return new Client
            {
                ClientId = clientId,
                Name = clientId switch
                {
                    1 => "VeryImportantClient",
                    2 => "ImportantClient",
                    _ => null
                }
            };
        }
    }
}