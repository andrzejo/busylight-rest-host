using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Busylight;
using BusylightRestHost.Utils;

namespace BusylightRestHost.Action
{
    public abstract class Action : IAction
    {
        protected readonly ISDK _sdk;
        protected readonly ActionParameters _parameters;

        protected Action(ISDK sdk, ActionParameters parameters)
        {
            _sdk = sdk;
            _parameters = parameters;
        }

        public abstract string Execute();

        protected static string Serialize(Type type, object obj)
        {
            var stream = new MemoryStream();
            var serializer = new DataContractJsonSerializer(type);
            serializer.WriteObject(stream, obj);
            return Streams.Read(stream);
        }
    }
}