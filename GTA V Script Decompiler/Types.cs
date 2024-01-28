using System;
using System.Collections.Generic;

namespace Decompiler
{
    public static class Types
    {
        public static readonly List<TypeInfo> typeInfos = new();

        public static readonly TypeInfo BOOL = new(4, "BOOL", "flag", "b");
        public static readonly TypeInfo PBOOL = new(4, "BOOL*", "p_bool", "pb");
        public static readonly TypeInfo FLOAT = new(3, "float", "num", "f");
        public static readonly TypeInfo INT = new(3, "int", "num", "i");
        //public static readonly TypeInfo STRING = new(4, "char[]", "str", "c");
        public static readonly TypeInfo STRING = new(4, "STRING", "str", "c");
        public static readonly TypeInfo PSTRING = new(4, "char*", "str", "s");
        public static readonly TypeInfo CPSTRING = new(4, "const char*", "str", "s");
        public static readonly TypeInfo UNKNOWN = new(0, "var", "unk", "u");
        public static readonly TypeInfo VEC3 = new(2, "Vector3", "vector", "v");
        public static readonly TypeInfo VOID = new(4, "void", "???", "???");

        public static readonly TypeInfo ANY = new(2, "Any", "any", "an");
        public static readonly TypeInfo BLIP = new(4, "Blip", "blip", "bl");
        public static readonly TypeInfo CAM = new(4, "Cam", "cam", "ca");
        public static readonly TypeInfo ENTITY = new(4, "Entity", "entity", "e");
        public static readonly TypeInfo FIREID = new(4, "FireId", "id", "fi");
        public static readonly TypeInfo HASH = new(4, "Hash", "hash", "h");
        public static readonly TypeInfo INTERIOR = new(4, "Interior", "interior", "in");
        public static readonly TypeInfo ITEMSET = new(4, "ItemSet", "itemset", "is");
        public static readonly TypeInfo OBJECT = new(5, "Object", "object", "ob");
        public static readonly TypeInfo PED = new(5, "Ped", "ped", "ped");
        public static readonly TypeInfo PICKUP = new(5, "Pickup", "pickup", "pk");
        public static readonly TypeInfo PLAYER = new(5, "Player", "player", "pl");
        public static readonly TypeInfo SCRHANDLE = new(4, "ScrHandle", "handle", "sh");
        public static readonly TypeInfo VEHICLE = new(5, "Vehicle", "vehicle", "ve");
        public static readonly TypeInfo FUNCTION = new(5, "function", "func", "func");

        // RDR stuff

        public static readonly TypeInfo VARIADIC = new(4, "", "", "");
        public static readonly TypeInfo POPZONE = new(4, "PopZone", "zone", "pz");
        public static readonly TypeInfo VOLUME = new(4, "Volume", "volume", "vol");
        public static readonly TypeInfo PROPSET = new(4, "PropSet", "set", "prs");
        public static readonly TypeInfo ANIMSCENE = new(4, "AnimScene", "scene", "ans");
        public static readonly TypeInfo PERSCHAR = new(4, "PersChar", "char", "pch");
        public static readonly TypeInfo PROMPT = new(4, "Prompt", "prompt", "pmt");

        public static readonly TypeInfo ECONTROLTYPE = new(6, "eControlType", "control", "ect", new(typeof(Enums.PadControlType)));
        public static readonly TypeInfo ECONTROLACTION = new(4, "eControlAction", "action", "eca", new(typeof(Enums.PadControlAction)));
        public static readonly TypeInfo EHUDCOMPONENT = new(6, "eHudComponent", "component", "ehc", new(typeof(Enums.HudComponent)));
        public static readonly TypeInfo EPEDTYPE = new(6, "ePedType", "type", "ept", new(typeof(Enums.PedType)));
        public static readonly TypeInfo EPEDCOMPONENTTYPE = new(6, "ePedComponentType", "type", "epct", new(typeof(Enums.PedComponentType)));
        public static readonly TypeInfo ESTACKSIZE = new(6, "eStackSize", "stackSize", "ess", new(typeof(Enums.StackSize)));
        public static readonly TypeInfo EDECORATORTYPE = new(6, "eDecoratorType", "decorType", "edt", new(typeof(Enums.DecoratorType)));
        public static readonly TypeInfo EEVENTGROUP = new(6, "eEventGroup", "group", "eeg", new(typeof(Enums.EventGroup)));
        public static readonly TypeInfo EHUDCOLOUR = new(6, "eHudColour", "color", "ehc", new(typeof(Enums.HudColour)));
        public static readonly TypeInfo EBLIPSPRITE = new(6, "eBlipSprite", "sprite", "ebs", new(typeof(Enums.BlipSprite)));
        public static readonly TypeInfo EKNOCKOFFVEHICLE = new(6, "eKnockOffVehicle", "knockOffState", "eknv", new(typeof(Enums.KnockOffVehicle)));
        public static readonly TypeInfo ECOMBATMOVEMENT = new(6, "eCombatMovement", "movement", "ecm", new(typeof(Enums.CombatMovement)));
        public static readonly TypeInfo ECOMBATATTRIBUTE = new(6, "eCombatAttribute", "attribute", "ecat", new(typeof(Enums.CombatAttribute)));
        public static readonly TypeInfo ECHARACTER = new(4, "eCharacter", "character", "ech", new(typeof(Enums.Character)));
        public static readonly TypeInfo ETRANSITIONSTATE = new(6, "eTransitionState", "state", "ets", new(typeof(Enums.TransitionState)));
        public static readonly TypeInfo EDISPATCHTYPE = new(6, "eDispatchType", "type", "edt", new(typeof(Enums.DispatchType)));
        public static readonly TypeInfo ELEVELINDEX = new(6, "eLevelIndex", "index", "eli", new(typeof(Enums.LevelIndex)));
        public static readonly TypeInfo EVIEWMODECONTEXT = new(6, "eViewModeContext", "context", "evmc", new(typeof(Enums.ViewModeContext)));
        public static readonly TypeInfo ETHREADPRIORITY = new(6, "eThreadPriority", "priority", "etp", new(typeof(Enums.ThreadPriority)));
        public static readonly TypeInfo ESETPLAYERCONTROLFLAGS = new(6, "eSetPlayerControlFlags", "controlBs", "espcf", new(typeof(Enums.SetPlayerControlFlag), true));
        public static readonly TypeInfo ESCRIPTLOOKATFLAGS = new(6, "eScriptLookAtFlags", "lookatFlags", "eslf", new(typeof(Enums.ScriptLookatFlags), true));
        public static readonly TypeInfo ESCRIPTTASKHASH = new(6, "eScriptTaskHash", "taskHash", "esth", new(typeof(Enums.ScriptTaskHash)));
        public static readonly TypeInfo EMPSTAT = new(6, "eMPStat", "stat", "emps", new(typeof(Enums.MPStat)));
        public static readonly TypeInfo EEVENTTYPE = new(6, "eEventType", "type", "eet", new(typeof(Enums.EventType)));
        public static readonly TypeInfo EVIEWMODE = new(6, "eViewMode", "mode", "evm", new(typeof(Enums.ViewMode)));

        public static TypeInfo GetFromName(string name)
        {
            foreach (var d in typeInfos)
            {
                if (d.SingleName.ToLower() == name.ToLower())
                    return d;
            }

            throw new Exception("Unknown type");
        }

        public class TypeInfo
        {
            public int Precedence;
            public string SingleName;
            public string AutoName;
            public ScriptEnum? Enum = null;
            public string Prefix;
            private TypeContainer sealedContainer;
            public bool IsPointerType { get; private set; }
            public TypeInfo ChildPtr;
            public TypeInfo ParentPtr;

            public TypeInfo(int precedence, string singlename, string autoname, string prefix, ScriptEnum? @enum = null, TypeInfo parent = null)
            {
                Precedence = precedence;
                SingleName = singlename;
                AutoName = autoname;
                Enum = @enum;
                Prefix = prefix;
                ParentPtr = parent;

                Types.typeInfos.Add(this);
                sealedContainer = new(this, true);

                if (ParentPtr == null)
                    _ = GetPointerVersion();
            }

            public string ReturnType => SingleName + " ";

            public string VarDec => SingleName + " ";

            public string ArrayDec => SingleName + "[] ";

            public static bool operator >(TypeInfo a, TypeInfo b) => a.Precedence > b.Precedence;

            public static bool operator <(TypeInfo a, TypeInfo b) => a.Precedence < b.Precedence;

            /// <summary>
            /// Gets a reference to a <b>SEALED</b> container containing the type
            /// </summary>
            public ref TypeContainer GetContainer() => ref sealedContainer;

            public TypeInfo GetPointerVersion()
            {
                ChildPtr ??= new(Precedence, SingleName + "*", "p_" + AutoName, "p" + Prefix, Enum, this);

                return ChildPtr;
            }

            public TypeInfo? TryGetLiteralVersion() => ParentPtr ?? null;
        }
    }

    /// <summary>
    /// A "container" to hold a type. The container will attempt to propagate itself throught the AST and can even jump across functions
    /// </summary>
    public class TypeContainer
    {
        public Types.TypeInfo Type { get; private set; }

        private bool IsSealed;

        public TypeContainer(Types.TypeInfo defaultType = null, bool isSealed = false)
        {
            Type = defaultType ?? Types.UNKNOWN;
            IsSealed = isSealed;
        }

        public void HintType(Types.TypeInfo type)
        {
            if (IsSealed)
                return;

            if (Type < type)
                Type = type;
        }

        /// <summary>
        /// The reference passed WILL be overwritten unless it is sealed
        /// </summary>
        public void HintType(ref TypeContainer container)
        {
            if ((!IsSealed) && Type < container.Type)
                Type = container.Type;

            else if (IsSealed && (!container.IsSealed) && Type > container.Type)
                container.Type = Type;

            if (!(container.IsSealed || IsSealed))
                container = this;
        }

        /// <summary>
        /// Sealing a container makes the underlying type constant and stops propagation
        /// </summary>
        public void SealType() => IsSealed = true;
    }
}
