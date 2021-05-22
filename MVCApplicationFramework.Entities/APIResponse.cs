using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCApplicationFramework.Entities
{
    public class APIResponse
    {
        public string MessageCode { set; get; }
        public string MessageText { set; get; }
        public EnumMessageType MessageType { set; get; }
        public bool HasException { set; get; }
        public string Exception { set; get; }

        private List<Object> _Data;
        public List<Object> Data
        {
            get
            {
                return _Data;
            }
            set
            {
                _Data = value;
            }
        }

        public void Add(Object p_Object)
        {
            if (_Data == null)
            {
                _Data = new List<Object>();
            }
            _Data.Add(p_Object);
        }
    }

    public enum EnumMessageType
    {
        OPERATION_SUCCESS = 1, OPERATION_TECHNICAL_ERROR = 2, OPERATION_APPLICATION_ERROR = 3
    }
}
