using BusylightRestHost.Action;


namespace BusylightRestHost
{
    public class BusylightController
    {
        private readonly Factory _factory;

        public BusylightController()
        {
            _factory = new Factory(new BusylightLibProvider());
        }

        public string RunAction(ActionData actionData)
        {
            return _factory.Create(actionData).Execute();
        }
    }
}