using NLua;
using System.Collections.Generic;
using Lost_Island_Ranal.ECS;


//-----------------------------------------------------------------------------
// Created by: Ayran Olckers AKA The Geekiest One
// -2019-
// -Game Development Project-
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
//-----------------------------------------------------------------------------


    /// <summary>
    /// Dialogue 
    /// </summary>

namespace Lost_Island_Ranal.Graphics
{
    class Dialog_Option {
        public string Value { get; set; }
        public int NextDialogText { get; set; } = 0;
        public LuaFunction action = null;
    }

    class Dialog_Text {
        /*
         * String
         * List of options (if any)
         * Link to next dialog text
         */
        public string Value { get; set; } = "";
        public int NextDialogText { get; set; } = 0;
        public List<Dialog_Option> options = new List<Dialog_Option>();
    }

    class Dialogue
    {
        public Dictionary<int, Dialog_Text> Dialog_Texts = new Dictionary<int, Dialog_Text>();
        public int CurrentDialogText {get; set;} = 0;

        public Entity Speaker { get; set; } = null;
        public Entity Target { get; set; } = null;

        public Dialogue() {}
        public Dialogue(LuaTable table) {
            foreach(var dialog_text_table in table.Keys) {
                var dt = table[dialog_text_table] as LuaTable;
                var text = dt[1] as string;

                var dialog_text = new Dialog_Text{Value = text};

                if (dt[2] is LuaTable) {
                    var options = dt[2] as LuaTable;

                    foreach(var _option in options.Values) {
                        var op = _option as LuaTable;

                        var value = op[1] as string;
                        var next = (int)(op[2] as double?);

                        var dialog_option = new Dialog_Option
                        {
                            Value = value,
                            NextDialogText = next
                        };

                        if (op["action"] is LuaFunction)
                        {
                            dialog_option.action = op["action"] as LuaFunction;
                        }

                        dialog_text.options.Add(dialog_option);

                    }

                } else if (dt[2] is double) {
                    var next = (int)(dt[2] as double?);
                    dialog_text.NextDialogText = next;
                }

                Dialog_Texts.Add((int)(dialog_text_table as double?), dialog_text);
            }
        }
    }
}
