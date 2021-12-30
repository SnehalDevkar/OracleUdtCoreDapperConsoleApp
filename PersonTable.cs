using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace OracleUDTCoreDapperConsoleApp
{
    public class PersonTable : INullable, IOracleCustomType
    {
        private bool m_IsNull;
        private Person[] person;
        public virtual bool IsNull
        {
            get
            {
                return m_IsNull;
            }
        }
        public static PersonTable Null
        {
            get
            {
                PersonTable p = new PersonTable();
                p.m_IsNull = true;
                return p;
            }
        }

        [OracleArrayMappingAttribute()]
        public virtual Person[] Value
        {
            get
            {
                return this.person;
            }
            set
            {
                this.person = value;
            }
        }

        public virtual void FromCustomObject(OracleConnection con, object pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, this.person);
        }

        public virtual void ToCustomObject(OracleConnection con, object pUdt)
        {
            this.person = ((Person[])(OracleUdt.GetValue(con, pUdt, 0)));
        }

        public override string ToString() { return ""; }

    }
}
