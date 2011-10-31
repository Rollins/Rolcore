
namespace Rolcore.Geography
{
    public class State
    {

        public string StateName { get; set; }

        public string StateAbbreviation { get; set; }

        public State(string stateAbbreviation, string stateName)
        {
            this.StateName = stateName;
            this.StateAbbreviation = stateAbbreviation;
        }
    }
}
