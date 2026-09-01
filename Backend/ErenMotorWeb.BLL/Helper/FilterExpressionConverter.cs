using System.Linq.Expressions;

namespace ErenMotorWeb.BLL.Helper
{
    public static class FilterExpressionConverter<TEntity, TDto>
    {
        public static Expression<Func<TEntity, bool>> Convert(Expression<Func<TDto, bool>> filter)
        {
            var parameter = Expression.Parameter(typeof(TEntity), filter.Parameters[0].Name);
            var body = new ParameterReplacer(parameter).Visit(filter.Body);
            return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        }

        private class ParameterReplacer : ExpressionVisitor
        {
            private readonly ParameterExpression _parameter;

            public ParameterReplacer(ParameterExpression parameter)
            {
                _parameter = parameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return _parameter;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                if (node.Member.DeclaringType == typeof(TDto))
                {
                    var member = typeof(TEntity).GetProperty(node.Member.Name);
                    if (member == null)
                    {
                        throw new InvalidOperationException($"Property '{node.Member.Name}' not found on entity type '{typeof(TEntity).Name}'");
                    }
                    return Expression.Property(_parameter, member);
                }
                return base.VisitMember(node);
            }
        }
    }
}
