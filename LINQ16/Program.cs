using System;
using System.Linq.Expressions;

namespace LINQ16.Shared
{
    internal class Class
    {
        public static void Main(string[] args)
        {
            Expression03();
            Console.ReadKey();
        }

        private static void Expression01()
        {
            Func<int, bool> IsEven = num => num % 2 == 0;
            Console.WriteLine(IsEven(10));

            Expression<Func<int, bool>> IsEventExpression = num => num % 2 == 0;
            Func<int, bool> IsEventV2 = IsEventExpression.Compile();
            Console.WriteLine(IsEventV2(10));
        }
        private static void Expression02()
        {
            Expression<Func<int, bool>> IsNegativeExpression = (num) => num < 0;

            ParameterExpression numParam = IsNegativeExpression.Parameters[0];
            BinaryExpression operaton = (BinaryExpression)IsNegativeExpression.Body;
            ParameterExpression left = (ParameterExpression)operaton.Left;
            ConstantExpression right = (ConstantExpression)operaton.Right;
            Console.WriteLine($"Decomposed Expression : {numParam.Name}=>{left.Name}{operaton.NodeType}{right.Value}");
        }
        private static void Expression03()
        {
            // (num) => num % 2 = 0 ;
            ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
            ConstantExpression zeroParam = Expression.Constant(0, typeof(int));
            ConstantExpression twoParam = Expression.Constant(2, typeof(int));
            BinaryExpression moduleBinaryExpression = Expression.Modulo(numParam, twoParam);
            BinaryExpression isEvenBinaryExpression = Expression.Equal(moduleBinaryExpression, zeroParam);
            Expression<Func<int, bool>> IsEveExpression = 
                Expression.Lambda<Func<int, bool>>(isEvenBinaryExpression, new ParameterExpression[] { numParam });
        }
    }
}