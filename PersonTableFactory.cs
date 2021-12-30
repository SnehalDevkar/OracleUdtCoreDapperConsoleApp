using Oracle.ManagedDataAccess.Types;

namespace OracleUDTCoreDapperConsoleApp
{
    [OracleCustomTypeMappingAttribute("ODP_OBJ1_SAMPLE_PERSON_TABLE")]

    public class PersonTableFactory : IOracleCustomTypeFactory, IOracleArrayTypeFactory
    {
        public virtual IOracleCustomType CreateObject()
        {
            PersonTable obj = new PersonTable();
            return obj;
        }

        public virtual System.Array CreateArray(int length)
        {
            Person[] collElem = new Person[length];
            return collElem;
        }

        public virtual System.Array CreateStatusArray(int length)
        {
            return null;
        }
    }
}
