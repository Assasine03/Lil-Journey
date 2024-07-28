
public class TweenService : Service
{

}

public class Service
{
    public int id { get; private set; }
    public string service { get; private set; }

    public Service(int _id = 0, string _service = "null")
    {
        id = _id;
        service = _service;
    }
}

public class ASGame
{
    public int serviceID { get; set; }
    public Service[] services; 

    public ASGame()
    {

    }

    public void CreateService()
    {

    }
    public Service GetService(string serviceNameOrId)
    {
        for (int i = 0; i < services.Length; i++)
        {
            if (serviceNameOrId == services[i].service)
            {
                return services[i];
            }
        }
        return new Service(0, "Service not Found");
    }


}
