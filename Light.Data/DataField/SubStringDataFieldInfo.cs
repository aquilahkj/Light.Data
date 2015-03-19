using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Mappings;

namespace Light.Data.DataField
{
    class SubStringDataFieldInfo : ExtendDataFieldInfo
    {
        int _start = 0;

        int _size = 0;

        //internal SubStringDataFieldInfo(DataFieldMapping fieldMapping, int start, int size)
        //    : base(fieldMapping)
        //{
        //    if (fieldMapping.ObjectType != typeof(string))
        //    {
        //        throw new LightDataException(string.Format(RE.TypeUnsupportTheTransform, fieldMapping.ObjectType));
        //    }
        //    if (start <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException("start");
        //    }
        //    if (size < 0)
        //    {
        //        throw new ArgumentOutOfRangeException("size");
        //    }
        //    _start = start;
        //    _size = size;
        //}

        internal SubStringDataFieldInfo(DataFieldInfo info, int start, int size)
            : base(info)
        {
         if (start <= 0)
            {
                throw new ArgumentOutOfRangeException("start");
            }
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException("size");
            }
            _start = start;
            _size = size;
        }

        internal override string CreateDataFieldSql(CommandFactory factory, bool isFullName)
        {
            string field = BaseFieldInfo.CreateDataFieldSql(factory, isFullName);
            return factory.CreateSubStringSql(field, _start, _size);
        }

        internal override string DBType
        {
            get
            {
                return string.Empty;
            }
        }

        protected override bool EqualsDetail(DataFieldInfo info)
        {
            if (base.EqualsDetail(info))
            {
                SubStringDataFieldInfo target = info as SubStringDataFieldInfo;
                return this._start == target._start && this._size == target._size;
            }
            else
            {
                return false;
            }
        }

        //public override bool Equals(object obj)
        //{
        //    bool result = base.Equals(obj);
        //    if (!result)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        SubStringDataFieldInfo target = obj as SubStringDataFieldInfo;
        //        if (Object.Equals(target, null))
        //        {
        //            return false;
        //        }
        //        return this._start == target._start && this._size == target._size;
        //    }
        //}

        //public override int GetHashCode()
        //{
        //    int hash = base.GetHashCode();
        //    hash ^= (_start * "_start".GetHashCode());
        //    hash ^= (_size * "_size".GetHashCode());
        //    return hash;
        //}
    }
}
