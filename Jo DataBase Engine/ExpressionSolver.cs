using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JoDataBaseEngine
{
    static class ExpressionSolver
    {
        public static string LambdaToString<T>(Expression<Func<T, bool>> expression)
        {

            var replacements = new Dictionary<string, string>();
            WalkExpression(replacements, expression);

            string body = expression.Body.ToString();

            foreach (var parm in expression.Parameters)
            {
                var parmName = parm.Name;
                body = body.Replace(parmName + ".", "");
                
                body = body.Replace("AndAlso", "AND");
                body = body.Replace("OrElse", "OR");
                body = body.Replace("==", "=");
            }

            foreach (var replacement in replacements)
            {
                body = body.Replace(replacement.Key, replacement.Value);
            }

            foreach (var parm in expression.Parameters)
            {
                var props = parm.Type.GetProperties();

                foreach (PropertyInfo prp in props)
                {
                    var attributes = prp.GetCustomAttributes(false);

                    //Column Name
                    var columnattributes = attributes.Where(a => a.GetType() == typeof(DbColumnAttribute));
                    var column = columnattributes.FirstOrDefault(a => a.GetType() == typeof(DbColumnAttribute));
                    DbColumnAttribute cl = column as DbColumnAttribute;
                    if (!(cl is null))
                        body = body.Replace(prp.Name, cl.Name);
                }
            }
            return body;
        }

        private static void WalkExpression(Dictionary<string, string> replacements, Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    string replacementExpression = expression.ToString();
                    if (replacementExpression.Contains("value("))
                    {
                        string replacementValue = Expression.Lambda(expression).Compile().DynamicInvoke().ToString();
                        if (!replacements.ContainsKey(replacementExpression))
                        {
                            replacements.Add(replacementExpression, replacementValue.ToString());
                        }
                    }
                    break;

                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.OrElse:
                case ExpressionType.AndAlso:
                case ExpressionType.NotEqual:
                case ExpressionType.Equal:
                    var bexp = expression as BinaryExpression;
                    WalkExpression(replacements, bexp.Left);
                    WalkExpression(replacements, bexp.Right);
                    break;

                case ExpressionType.Call:
                    var mcexp = expression as MethodCallExpression;
                    foreach (var argument in mcexp.Arguments)
                    {
                        WalkExpression(replacements, argument);
                    }
                    break;

                case ExpressionType.Lambda:
                    var lexp = expression as LambdaExpression;
                    WalkExpression(replacements, lexp.Body);
                    break;

                case ExpressionType.Constant:
                    //do nothing
                    break;

                default:
                    Trace.WriteLine("Unknown type");
                    break;
            }
        }
    }
}
