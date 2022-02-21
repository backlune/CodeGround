using System;
using System.Collections.Generic;
using System.Text;

namespace GodeGround.Base.CSharpVersions
{
    public record Person2
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }

    public record Person(string FirstName, string LastName);

    internal class Nine
    {
        void Records()
        {
            Person p = new("Joe", "Doe");
            //p.FirstName = "test"; //Not allowed as only init by default
            Person p1 = new(null, null);
        }

    }
}
