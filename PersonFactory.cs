using Oracle.ManagedDataAccess.Types;

namespace OracleUDTCoreDapperConsoleApp
{
    /* PersonFactory Class
       An instance of the PersonFactory class is used to create Person objects
    */
    [OracleCustomTypeMappingAttribute("ODP_OBJ1_SAMPLE_PERSON_TYPE")]
    public class PersonFactory : IOracleCustomTypeFactory
    {
        // Implementation of IOracleCustomTypeFactory.CreateObject()
        public IOracleCustomType CreateObject()
        {
            // Return a new custom object
            return new Person();
        }
    }
}
