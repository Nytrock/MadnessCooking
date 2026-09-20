using Newtonsoft.Json;
using System;

namespace MadnessCooking.General {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class GeneralUpgradeData : ISaveable {

    }
}
