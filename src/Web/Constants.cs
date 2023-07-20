namespace Web
{
    public class Constants
    {
        
        public Constants(IConfiguration config)
        {
            Web = config.GetValue<string>("Web");
            Identity = config.GetValue<string>("Identity");
            CollectionService = config.GetValue<string>("CollectionService");
        }

            public string Web;
            public string Identity;
            public  string CollectionService;
    }
}