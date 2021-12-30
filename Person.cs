using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace OracleUDTCoreDapperConsoleApp
{
    public class Person : INullable, IOracleCustomType
    {
        private bool m_bIsNull;  // Whether the Person object is NULL    
        private string m_name;     // "NAME" attribute  
        private string m_address;  // "ADDRESS" attribute  
        private int? m_age;      // "AGE" attribute

        // Implementation of INullable.IsNull
        public virtual bool IsNull
        {
            get
            {
                return m_bIsNull;
            }
        }

        // Person.Null is used to return a NULL Person object
        public static Person Null
        {
            get
            {
                Person p = new Person();
                p.m_bIsNull = true;
                return p;
            }
        }

        // Specify the OracleObjectMappingAttribute to map "Name" to "NAME"
        [OracleObjectMappingAttribute("NAME")]
        // The mapping can also be specified using attribute index 0
        // [OracleObjectMappingAttribute(0)]
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        // Specify the OracleObjectMappingAttribute to map "Address" to "ADDRESS"
        [OracleObjectMappingAttribute("ADDRESS")]
        // The mapping can also be specified using attribute index 1
        // [OracleObjectMappingAttribute(1)]
        public string Address
        {
            get
            {
                return m_address;
            }
            set
            {
                m_address = value;
            }
        }

        // Specify the OracleObjectMappingAttribute to map "Age" to "AGE"
        [OracleObjectMappingAttribute("AGE")]
        // The mapping can also be specified using attribute index 2
        // [OracleObjectMappingAttribute(2)]

        public int? Age
        {
            get
            {
                return m_age;
            }
            set
            {
                m_age = value;
            }
        }

        // Implementation of IOracleCustomType.FromCustomObject()
        public virtual void FromCustomObject(OracleConnection con, object pUdt)
        {
            // Convert from the Custom Type to Oracle Object

            // Set the "NAME" attribute.     
            // By default the "NAME" attribute will be set to NULL
            if (m_name != null)
            {
                OracleUdt.SetValue(con, pUdt, "NAME", m_name);
                // The "NAME" attribute can also be accessed by specifying index 0
                // OracleUdt.SetValue(con, pUdt, 0, m_name);
            }

            // Set the "ADDRESS" attribute.     
            // By default the "ADDRESS" attribute will be set to NULL
            if (m_address!=null)
            {
                OracleUdt.SetValue(con, pUdt, "ADDRESS", m_address);
                // The "ADDRESS" attribute can also be accessed by specifying index 1
                // OracleUdt.SetValue(con, pUdt, 1, m_address);
            }

            // Set the "AGE" attribute.    

            // By default the "AGE" attribute will be set to NULL
            if (m_age != null)
            {
                OracleUdt.SetValue(con, pUdt, "AGE", m_age);
                // The "AGE attribute can also be accessed by specifying index 2
                // OracleUdt.SetValue(con, pUdt, 2, m_age);
            }

        }

        // Implementation of IOracleCustomType.ToCustomObject()
        public virtual void ToCustomObject(OracleConnection con, object pUdt)
        {
            // Convert from the Oracle Object to a Custom Type

            // Get the "NAME" attribute
            // If the "NAME" attribute is NULL, then null will be returned
            m_name = (string)OracleUdt.GetValue(con, pUdt, "NAME");

            // The "NAME" attribute can also be accessed by specifying index 0
            // m_name = (string)OracleUdt.GetValue(con, pUdt, 0);

            // Get the "ADDRESS" attribute
            // If the "ADDRESS" attribute is NULL, then OracleString.Null will be returned
            m_address = (string)OracleUdt.GetValue(con, pUdt, "ADDRESS");

            // The "NAME" attribute can also be accessed by specifying index 1
            // m_address = (OracleString)OracleUdt.GetValue(con, pUdt, 1);

            // Get the "AGE" attribute

            // If the "AGE" attribute is NULL, then null will  be returned
            m_age = (int?)OracleUdt.GetValue(con, pUdt, "AGE");
            // The "AGE" attribute can also be accessed by specifying index 2
            // m_age = (int?)OracleUdt.GetValue(con, pUdt, 2);    

        }

        public override string ToString()
        {
            // Return a string representation of the custom object
            if (m_bIsNull)
                return "Person.Null";
            else
            {
                string name = (m_name == null) ? "NULL" : m_name;
                string address = (m_address == null) ? "NULL" : m_address;
                string age = (m_age == null) ? "NULL" : m_age.ToString();

                return "Person(" + name + ", " + address + ", " + age + ")";
            }
        }
    }
}
