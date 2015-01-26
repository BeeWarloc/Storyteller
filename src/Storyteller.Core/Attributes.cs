﻿using System;
using Storyteller.Core.Model;

namespace Storyteller.Core
{
    public abstract class ModifyCellAttribute : Attribute
    {
        public abstract void Modify(Cell cell);
    }

    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Method, Inherited = false)]
    public class AliasAsAttribute : ModifyCellAttribute
    {
        private readonly string _alias;

        public AliasAsAttribute(string alias)
        {
            _alias = alias;
        }

        public override void Modify(Cell cell)
        {
            cell.Key = _alias;
        }

        public string Alias { get { return _alias; } }
    }

    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Parameter | AttributeTargets.Property,
        AllowMultiple = false, Inherited = false)]
    public class HeaderAttribute : ModifyCellAttribute
    {
        private readonly string _header;

        public HeaderAttribute(string header)
        {
            _header = header;
        }

        public override void Modify(Cell cell)
        {
            cell.header = _header;
        }
    }

    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Parameter | AttributeTargets.Property,
        AllowMultiple = false, Inherited = false)
    ]
    public class DefaultAttribute : ModifyCellAttribute
    {
        private readonly string _value;

        public DefaultAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
            get
            {
                if (_value == "GUID") return Guid.NewGuid().ToString();


                return _value;
            }
        }

        public override void Modify(Cell cell)
        {
            cell.DefaultValue = _value;
        }


    }

    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Parameter | AttributeTargets.Property,
        AllowMultiple = false, Inherited = false)
    ]
    public class EditorAttribute : ModifyCellAttribute
    {
        private readonly string _editor;

        public EditorAttribute(string editor)
        {
            _editor = editor;
        }

        public string Editor
        {
            get
            {
                return _editor;
            }
        }

        public override void Modify(Cell cell)
        {
            cell.editor = _editor;
        }


    }
}