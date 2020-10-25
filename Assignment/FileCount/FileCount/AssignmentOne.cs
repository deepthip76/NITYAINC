namespace FileCount
{
    /// <summary>
    /// Class A
    /// </summary>
    public class A
    {
        /// <summary>
        /// Structure B with 'c' as struct member
        /// </summary>
        public struct B
        {
            public int c;
        };

        // Create object for the structure to access it's members
        public B b = new B();
    }

    /// <summary>
    /// Class C
    /// </summary>
    public class C
    {
        /// <summary>
        /// Structure B with 'a' as struct member
        /// </summary>
        public struct B
        {
            // Struct member
            public int a;
        }

        // Create object for the structure to access it's members
        public B b = new B();

    }

    /// <summary>
    /// Implementation of solution to the given problem
    /// </summary>
    public class Implementation
    {
        /// <summary>
        /// Method to implement the solution
        /// </summary>
        public void ImplementSolution()
        {
            // Create object for class A
            A a = new A();

            // Create object for Class C
            C c = new C();

            // Required code
            a.b.c = c.b.a;
        }
    }
}
