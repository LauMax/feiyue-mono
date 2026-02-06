using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Feiyue.Formatter.CSharp.SyntaxPrinter.SyntaxNodePrinters;
using Feiyue.Formatter.DocTypes;

#pragma warning disable RCS0056

namespace Feiyue.Formatter.CSharp.SyntaxPrinter;

internal static class Node
{
    public static Doc Print(SyntaxNode? syntaxNode, PrintingContext context)
    {
        if (syntaxNode is null)
            return Doc.Null;

        if (context.State.PrintingDepth > 200)
            throw new InTooDeepException();

        if (CSharpierIgnore.IsNodeIgnored(syntaxNode))
            return CSharpierIgnore.PrintWithoutFormatting(syntaxNode, context).Trim();

        context.State.PrintingDepth++;
        try
        {
            return (SyntaxKind)syntaxNode.RawKind switch
            {
                SyntaxKind.AliasQualifiedName => AliasQualifiedName.Print((AliasQualifiedNameSyntax)syntaxNode, context),
                SyntaxKind.AnonymousMethodExpression => AnonymousMethodExpression.Print((AnonymousMethodExpressionSyntax)syntaxNode, context),
                SyntaxKind.AnonymousObjectCreationExpression => AnonymousObjectCreationExpression.Print((AnonymousObjectCreationExpressionSyntax)syntaxNode, context),
                SyntaxKind.AnonymousObjectMemberDeclarator => AnonymousObjectMemberDeclarator.Print((AnonymousObjectMemberDeclaratorSyntax)syntaxNode, context),
                SyntaxKind.ArgumentList => ArgumentList.Print((ArgumentListSyntax)syntaxNode, context),
                SyntaxKind.Argument => Argument.Print((ArgumentSyntax)syntaxNode, context),
                SyntaxKind.ArrayCreationExpression => ArrayCreationExpression.Print((ArrayCreationExpressionSyntax)syntaxNode, context),
                SyntaxKind.ArrayRankSpecifier => ArrayRankSpecifier.Print((ArrayRankSpecifierSyntax)syntaxNode, context),
                SyntaxKind.ArrayType => ArrayType.Print((ArrayTypeSyntax)syntaxNode, context),
                SyntaxKind.ArrowExpressionClause => ArrowExpressionClause.Print((ArrowExpressionClauseSyntax)syntaxNode, context),
                SyntaxKind.SimpleAssignmentExpression
                or SyntaxKind.AddAssignmentExpression
                or SyntaxKind.SubtractAssignmentExpression
                or SyntaxKind.MultiplyAssignmentExpression
                or SyntaxKind.DivideAssignmentExpression
                or SyntaxKind.ModuloAssignmentExpression
                or SyntaxKind.AndAssignmentExpression
                or SyntaxKind.ExclusiveOrAssignmentExpression
                or SyntaxKind.OrAssignmentExpression
                or SyntaxKind.LeftShiftAssignmentExpression
                or SyntaxKind.RightShiftAssignmentExpression
                or SyntaxKind.UnsignedRightShiftAssignmentExpression
                or SyntaxKind.CoalesceAssignmentExpression => AssignmentExpression.Print((AssignmentExpressionSyntax)syntaxNode, context),
                SyntaxKind.AttributeList => AttributeList.Print((AttributeListSyntax)syntaxNode, context),
                SyntaxKind.AwaitExpression => AwaitExpression.Print((AwaitExpressionSyntax)syntaxNode, context),
                SyntaxKind.ExpressionColon or SyntaxKind.NameColon => BaseExpressionColon.Print((BaseExpressionColonSyntax)syntaxNode, context),
                SyntaxKind.BaseExpression => BaseExpression.Print((BaseExpressionSyntax)syntaxNode, context),
                SyntaxKind.FieldDeclaration or SyntaxKind.EventFieldDeclaration => BaseFieldDeclaration.Print((BaseFieldDeclarationSyntax)syntaxNode, context),
                SyntaxKind.MethodDeclaration
                or SyntaxKind.OperatorDeclaration
                or SyntaxKind.ConversionOperatorDeclaration
                or SyntaxKind.ConstructorDeclaration
                or SyntaxKind.DestructorDeclaration => BaseMethodDeclaration.Print((BaseMethodDeclarationSyntax)syntaxNode, context),
                SyntaxKind.PropertyDeclaration or SyntaxKind.EventDeclaration or SyntaxKind.IndexerDeclaration => BasePropertyDeclaration.Print(
                    (BasePropertyDeclarationSyntax)syntaxNode,
                    context),
                SyntaxKind.ClassDeclaration
                or SyntaxKind.StructDeclaration
                or SyntaxKind.InterfaceDeclaration
                or SyntaxKind.RecordDeclaration
                or SyntaxKind.RecordStructDeclaration
                or SyntaxKind.EnumDeclaration => BaseTypeDeclaration.Print((BaseTypeDeclarationSyntax)syntaxNode, context),
                SyntaxKind.AddExpression
                or SyntaxKind.SubtractExpression
                or SyntaxKind.MultiplyExpression
                or SyntaxKind.DivideExpression
                or SyntaxKind.ModuloExpression
                or SyntaxKind.LeftShiftExpression
                or SyntaxKind.RightShiftExpression
                or SyntaxKind.UnsignedRightShiftExpression
                or SyntaxKind.LogicalOrExpression
                or SyntaxKind.LogicalAndExpression
                or SyntaxKind.BitwiseOrExpression
                or SyntaxKind.BitwiseAndExpression
                or SyntaxKind.ExclusiveOrExpression
                or SyntaxKind.EqualsExpression
                or SyntaxKind.NotEqualsExpression
                or SyntaxKind.LessThanExpression
                or SyntaxKind.LessThanOrEqualExpression
                or SyntaxKind.GreaterThanExpression
                or SyntaxKind.GreaterThanOrEqualExpression
                or SyntaxKind.IsExpression
                or SyntaxKind.AsExpression
                or SyntaxKind.CoalesceExpression => BinaryExpression.Print((BinaryExpressionSyntax)syntaxNode, context),
                SyntaxKind.OrPattern or SyntaxKind.AndPattern => BinaryPattern.Print((BinaryPatternSyntax)syntaxNode, context),
                SyntaxKind.Block => Block.Print((BlockSyntax)syntaxNode, context),
                SyntaxKind.BracketedArgumentList => BracketedArgumentList.Print((BracketedArgumentListSyntax)syntaxNode, context),
                SyntaxKind.BracketedParameterList => BracketedParameterList.Print((BracketedParameterListSyntax)syntaxNode, context),
                SyntaxKind.BreakStatement => BreakStatement.Print((BreakStatementSyntax)syntaxNode, context),
                SyntaxKind.CasePatternSwitchLabel => CasePatternSwitchLabel.Print((CasePatternSwitchLabelSyntax)syntaxNode, context),
                SyntaxKind.CaseSwitchLabel => CaseSwitchLabel.Print((CaseSwitchLabelSyntax)syntaxNode, context),
                SyntaxKind.CastExpression => CastExpression.Print((CastExpressionSyntax)syntaxNode, context),
                SyntaxKind.CatchClause => CatchClause.Print((CatchClauseSyntax)syntaxNode, context),
                SyntaxKind.CheckedExpression or SyntaxKind.UncheckedExpression => CheckedExpression.Print((CheckedExpressionSyntax)syntaxNode, context),
                SyntaxKind.CheckedStatement or SyntaxKind.UncheckedStatement => CheckedStatement.Print((CheckedStatementSyntax)syntaxNode, context),
                SyntaxKind.ClassConstraint or SyntaxKind.StructConstraint => ClassOrStructConstraint.Print((ClassOrStructConstraintSyntax)syntaxNode, context),
                SyntaxKind.CollectionExpression => CollectionExpression.Print((CollectionExpressionSyntax)syntaxNode, context),
                SyntaxKind.ForEachStatement or SyntaxKind.ForEachVariableStatement => CommonForEachStatement.Print((CommonForEachStatementSyntax)syntaxNode, context),
                SyntaxKind.CompilationUnit => CompilationUnit.Print((CompilationUnitSyntax)syntaxNode, context),
                SyntaxKind.ConditionalAccessExpression => ConditionalAccessExpression.Print((ConditionalAccessExpressionSyntax)syntaxNode, context),
                SyntaxKind.ConditionalExpression => ConditionalExpression.Print((ConditionalExpressionSyntax)syntaxNode, context),
                SyntaxKind.ConstantPattern => ConstantPattern.Print((ConstantPatternSyntax)syntaxNode, context),
                SyntaxKind.ConstructorConstraint => ConstructorConstraint.Print((ConstructorConstraintSyntax)syntaxNode, context),
                SyntaxKind.ContinueStatement => ContinueStatement.Print((ContinueStatementSyntax)syntaxNode, context),
                SyntaxKind.DeclarationExpression => DeclarationExpression.Print((DeclarationExpressionSyntax)syntaxNode, context),
                SyntaxKind.DeclarationPattern => DeclarationPattern.Print((DeclarationPatternSyntax)syntaxNode, context),
                SyntaxKind.DefaultConstraint => DefaultConstraint.Print((DefaultConstraintSyntax)syntaxNode, context),
                SyntaxKind.DefaultExpression => DefaultExpression.Print((DefaultExpressionSyntax)syntaxNode, context),
                SyntaxKind.DefaultSwitchLabel => DefaultSwitchLabel.Print((DefaultSwitchLabelSyntax)syntaxNode, context),
                SyntaxKind.DelegateDeclaration => DelegateDeclaration.Print((DelegateDeclarationSyntax)syntaxNode, context),
                SyntaxKind.DiscardDesignation => DiscardDesignation.Print((DiscardDesignationSyntax)syntaxNode, context),
                SyntaxKind.DiscardPattern => DiscardPattern.Print((DiscardPatternSyntax)syntaxNode, context),
                SyntaxKind.DoStatement => DoStatement.Print((DoStatementSyntax)syntaxNode, context),
                SyntaxKind.ElementAccessExpression => ElementAccessExpression.Print((ElementAccessExpressionSyntax)syntaxNode, context),
                SyntaxKind.ElementBindingExpression => ElementBindingExpression.Print((ElementBindingExpressionSyntax)syntaxNode, context),
                SyntaxKind.ElseClause => ElseClause.Print((ElseClauseSyntax)syntaxNode, context),
                SyntaxKind.EmptyStatement => EmptyStatement.Print((EmptyStatementSyntax)syntaxNode, context),
                SyntaxKind.EnumMemberDeclaration => EnumMemberDeclaration.Print((EnumMemberDeclarationSyntax)syntaxNode, context),
                SyntaxKind.EqualsValueClause => EqualsValueClause.Print((EqualsValueClauseSyntax)syntaxNode, context),
                SyntaxKind.ExplicitInterfaceSpecifier => ExplicitInterfaceSpecifier.Print((ExplicitInterfaceSpecifierSyntax)syntaxNode, context),
                SyntaxKind.ExpressionElement => ExpressionElement.Print((ExpressionElementSyntax)syntaxNode, context),
                SyntaxKind.ExpressionStatement => ExpressionStatement.Print((ExpressionStatementSyntax)syntaxNode, context),
                SyntaxKind.ExternAliasDirective => ExternAliasDirective.Print((ExternAliasDirectiveSyntax)syntaxNode, context),
                SyntaxKind.FileScopedNamespaceDeclaration => FileScopedNamespaceDeclaration.Print((FileScopedNamespaceDeclarationSyntax)syntaxNode, context),
                SyntaxKind.FinallyClause => FinallyClause.Print((FinallyClauseSyntax)syntaxNode, context),
                SyntaxKind.FixedStatement => FixedStatement.Print((FixedStatementSyntax)syntaxNode, context),
                SyntaxKind.ForStatement => ForStatement.Print((ForStatementSyntax)syntaxNode, context),
                SyntaxKind.FromClause => FromClause.Print((FromClauseSyntax)syntaxNode, context),
                SyntaxKind.FunctionPointerType => FunctionPointerType.Print((FunctionPointerTypeSyntax)syntaxNode, context),
                SyntaxKind.GenericName => GenericName.Print((GenericNameSyntax)syntaxNode, context),
                SyntaxKind.GlobalStatement => GlobalStatement.Print((GlobalStatementSyntax)syntaxNode, context),
                SyntaxKind.GotoStatement or SyntaxKind.GotoCaseStatement or SyntaxKind.GotoDefaultStatement => GotoStatement.Print((GotoStatementSyntax)syntaxNode, context),
                SyntaxKind.GroupClause => GroupClause.Print((GroupClauseSyntax)syntaxNode, context),
                SyntaxKind.IdentifierName => IdentifierName.Print((IdentifierNameSyntax)syntaxNode, context),
                SyntaxKind.IfStatement => IfStatement.Print((IfStatementSyntax)syntaxNode, context),
                SyntaxKind.ImplicitArrayCreationExpression => ImplicitArrayCreationExpression.Print((ImplicitArrayCreationExpressionSyntax)syntaxNode, context),
                SyntaxKind.ImplicitElementAccess => ImplicitElementAccess.Print((ImplicitElementAccessSyntax)syntaxNode, context),
                SyntaxKind.ImplicitObjectCreationExpression => ImplicitObjectCreationExpression.Print((ImplicitObjectCreationExpressionSyntax)syntaxNode, context),
                SyntaxKind.ImplicitStackAllocArrayCreationExpression => ImplicitStackAllocArrayCreationExpression.Print(
                    (ImplicitStackAllocArrayCreationExpressionSyntax)syntaxNode,
                    context),
                SyntaxKind.IncompleteMember => IncompleteMember.Print((IncompleteMemberSyntax)syntaxNode, context),
                SyntaxKind.ObjectInitializerExpression
                or SyntaxKind.CollectionInitializerExpression
                or SyntaxKind.ArrayInitializerExpression
                or SyntaxKind.ComplexElementInitializerExpression
                or SyntaxKind.WithInitializerExpression => InitializerExpression.Print((InitializerExpressionSyntax)syntaxNode, context),
                SyntaxKind.InterpolatedStringExpression => InterpolatedStringExpression.Print((InterpolatedStringExpressionSyntax)syntaxNode, context),
                SyntaxKind.InterpolatedStringText => InterpolatedStringText.Print((InterpolatedStringTextSyntax)syntaxNode, context),
                SyntaxKind.Interpolation => Interpolation.Print((InterpolationSyntax)syntaxNode, context),
                SyntaxKind.InvocationExpression => InvocationExpression.Print((InvocationExpressionSyntax)syntaxNode, context),
                SyntaxKind.IsPatternExpression => IsPatternExpression.Print((IsPatternExpressionSyntax)syntaxNode, context),
                SyntaxKind.JoinClause => JoinClause.Print((JoinClauseSyntax)syntaxNode, context),
                SyntaxKind.LabeledStatement => LabeledStatement.Print((LabeledStatementSyntax)syntaxNode, context),
                SyntaxKind.LetClause => LetClause.Print((LetClauseSyntax)syntaxNode, context),
                SyntaxKind.ListPattern => ListPattern.Print((ListPatternSyntax)syntaxNode, context),
                SyntaxKind.ArgListExpression
                or SyntaxKind.NumericLiteralExpression
                or SyntaxKind.StringLiteralExpression
                or SyntaxKind.Utf8StringLiteralExpression
                or SyntaxKind.CharacterLiteralExpression
                or SyntaxKind.TrueLiteralExpression
                or SyntaxKind.FalseLiteralExpression
                or SyntaxKind.NullLiteralExpression
                or SyntaxKind.DefaultLiteralExpression => LiteralExpression.Print((LiteralExpressionSyntax)syntaxNode, context),
                SyntaxKind.LocalDeclarationStatement => LocalDeclarationStatement.Print((LocalDeclarationStatementSyntax)syntaxNode, context),
                SyntaxKind.LocalFunctionStatement => LocalFunctionStatement.Print((LocalFunctionStatementSyntax)syntaxNode, context),
                SyntaxKind.LockStatement => LockStatement.Print((LockStatementSyntax)syntaxNode, context),
                SyntaxKind.MakeRefExpression => MakeRefExpression.Print((MakeRefExpressionSyntax)syntaxNode, context),
                SyntaxKind.SimpleMemberAccessExpression or SyntaxKind.PointerMemberAccessExpression => MemberAccessExpression.Print(
                    (MemberAccessExpressionSyntax)syntaxNode,
                    context),
                SyntaxKind.MemberBindingExpression => MemberBindingExpression.Print((MemberBindingExpressionSyntax)syntaxNode, context),
                SyntaxKind.NameEquals => NameEquals.Print((NameEqualsSyntax)syntaxNode, context),
                SyntaxKind.NamespaceDeclaration => NamespaceDeclaration.Print((NamespaceDeclarationSyntax)syntaxNode, context),
                SyntaxKind.NullableType => NullableType.Print((NullableTypeSyntax)syntaxNode, context),
                SyntaxKind.ObjectCreationExpression => ObjectCreationExpression.Print((ObjectCreationExpressionSyntax)syntaxNode, context),
                SyntaxKind.OmittedArraySizeExpression => OmittedArraySizeExpression.Print((OmittedArraySizeExpressionSyntax)syntaxNode, context),
                SyntaxKind.OmittedTypeArgument => OmittedTypeArgument.Print((OmittedTypeArgumentSyntax)syntaxNode, context),
                SyntaxKind.OrderByClause => OrderByClause.Print((OrderByClauseSyntax)syntaxNode, context),
                SyntaxKind.ParameterList => ParameterList.Print((ParameterListSyntax)syntaxNode, context),
                SyntaxKind.Parameter => Parameter.Print((ParameterSyntax)syntaxNode, context),
                SyntaxKind.ParenthesizedExpression => ParenthesizedExpression.Print((ParenthesizedExpressionSyntax)syntaxNode, context),
                SyntaxKind.ParenthesizedLambdaExpression => ParenthesizedLambdaExpression.Print((ParenthesizedLambdaExpressionSyntax)syntaxNode, context),
                SyntaxKind.ParenthesizedPattern => ParenthesizedPattern.Print((ParenthesizedPatternSyntax)syntaxNode, context),
                SyntaxKind.ParenthesizedVariableDesignation => ParenthesizedVariableDesignation.Print((ParenthesizedVariableDesignationSyntax)syntaxNode, context),
                SyntaxKind.PointerType => PointerType.Print((PointerTypeSyntax)syntaxNode, context),
                SyntaxKind.PostIncrementExpression or SyntaxKind.PostDecrementExpression or SyntaxKind.SuppressNullableWarningExpression => PostfixUnaryExpression.Print(
                    (PostfixUnaryExpressionSyntax)syntaxNode,
                    context),
                SyntaxKind.PredefinedType => PredefinedType.Print((PredefinedTypeSyntax)syntaxNode, context),
                SyntaxKind.UnaryPlusExpression
                or SyntaxKind.UnaryMinusExpression
                or SyntaxKind.BitwiseNotExpression
                or SyntaxKind.LogicalNotExpression
                or SyntaxKind.PreIncrementExpression
                or SyntaxKind.PreDecrementExpression
                or SyntaxKind.AddressOfExpression
                or SyntaxKind.PointerIndirectionExpression
                or SyntaxKind.IndexExpression => PrefixUnaryExpression.Print((PrefixUnaryExpressionSyntax)syntaxNode, context),
                SyntaxKind.PrimaryConstructorBaseType => PrimaryConstructorBaseType.Print((PrimaryConstructorBaseTypeSyntax)syntaxNode, context),
                SyntaxKind.QualifiedName => QualifiedName.Print((QualifiedNameSyntax)syntaxNode, context),
                SyntaxKind.QueryBody => QueryBody.Print((QueryBodySyntax)syntaxNode, context),
                SyntaxKind.QueryContinuation => QueryContinuation.Print((QueryContinuationSyntax)syntaxNode, context),
                SyntaxKind.QueryExpression => QueryExpression.Print((QueryExpressionSyntax)syntaxNode, context),
                SyntaxKind.RangeExpression => RangeExpression.Print((RangeExpressionSyntax)syntaxNode, context),
                SyntaxKind.RecursivePattern => RecursivePattern.Print((RecursivePatternSyntax)syntaxNode, context),
                SyntaxKind.RefExpression => RefExpression.Print((RefExpressionSyntax)syntaxNode, context),
                SyntaxKind.RefTypeExpression => RefTypeExpression.Print((RefTypeExpressionSyntax)syntaxNode, context),
                SyntaxKind.RefType => RefType.Print((RefTypeSyntax)syntaxNode, context),
                SyntaxKind.RefValueExpression => RefValueExpression.Print((RefValueExpressionSyntax)syntaxNode, context),
                SyntaxKind.RelationalPattern => RelationalPattern.Print((RelationalPatternSyntax)syntaxNode, context),
                SyntaxKind.ReturnStatement => ReturnStatement.Print((ReturnStatementSyntax)syntaxNode, context),
                SyntaxKind.SelectClause => SelectClause.Print((SelectClauseSyntax)syntaxNode, context),
                SyntaxKind.SimpleBaseType => SimpleBaseType.Print((SimpleBaseTypeSyntax)syntaxNode, context),
                SyntaxKind.SimpleLambdaExpression => SimpleLambdaExpression.Print((SimpleLambdaExpressionSyntax)syntaxNode, context),
                SyntaxKind.SingleVariableDesignation => SingleVariableDesignation.Print((SingleVariableDesignationSyntax)syntaxNode, context),
                SyntaxKind.SizeOfExpression => SizeOfExpression.Print((SizeOfExpressionSyntax)syntaxNode, context),
                SyntaxKind.SlicePattern => SlicePattern.Print((SlicePatternSyntax)syntaxNode, context),
                SyntaxKind.SpreadElement => SpreadElement.Print((SpreadElementSyntax)syntaxNode, context),
                SyntaxKind.StackAllocArrayCreationExpression => StackAllocArrayCreationExpression.Print((StackAllocArrayCreationExpressionSyntax)syntaxNode, context),
                SyntaxKind.SwitchExpression => SwitchExpression.Print((SwitchExpressionSyntax)syntaxNode, context),
                SyntaxKind.SwitchSection => SwitchSection.Print((SwitchSectionSyntax)syntaxNode, context),
                SyntaxKind.SwitchStatement => SwitchStatement.Print((SwitchStatementSyntax)syntaxNode, context),
                SyntaxKind.ThisExpression => ThisExpression.Print((ThisExpressionSyntax)syntaxNode, context),
                SyntaxKind.ThrowExpression => ThrowExpression.Print((ThrowExpressionSyntax)syntaxNode, context),
                SyntaxKind.ThrowStatement => ThrowStatement.Print((ThrowStatementSyntax)syntaxNode, context),
                SyntaxKind.TryStatement => TryStatement.Print((TryStatementSyntax)syntaxNode, context),
                SyntaxKind.TupleElement => TupleElement.Print((TupleElementSyntax)syntaxNode, context),
                SyntaxKind.TupleExpression => TupleExpression.Print((TupleExpressionSyntax)syntaxNode, context),
                SyntaxKind.TupleType => TupleType.Print((TupleTypeSyntax)syntaxNode, context),
                SyntaxKind.TypeArgumentList => TypeArgumentList.Print((TypeArgumentListSyntax)syntaxNode, context),
                SyntaxKind.TypeConstraint => TypeConstraint.Print((TypeConstraintSyntax)syntaxNode, context),
                SyntaxKind.TypeOfExpression => TypeOfExpression.Print((TypeOfExpressionSyntax)syntaxNode, context),
                SyntaxKind.TypeParameterConstraintClause => TypeParameterConstraintClause.Print((TypeParameterConstraintClauseSyntax)syntaxNode, context),
                SyntaxKind.TypeParameterList => TypeParameterList.Print((TypeParameterListSyntax)syntaxNode, context),
                SyntaxKind.TypeParameter => TypeParameter.Print((TypeParameterSyntax)syntaxNode, context),
                SyntaxKind.TypePattern => TypePattern.Print((TypePatternSyntax)syntaxNode, context),
                SyntaxKind.NotPattern => UnaryPattern.Print((UnaryPatternSyntax)syntaxNode, context),
                SyntaxKind.UnsafeStatement => UnsafeStatement.Print((UnsafeStatementSyntax)syntaxNode, context),
                SyntaxKind.UsingDirective => UsingDirective.Print((UsingDirectiveSyntax)syntaxNode, context),
                SyntaxKind.UsingStatement => UsingStatement.Print((UsingStatementSyntax)syntaxNode, context),
                SyntaxKind.VariableDeclaration => VariableDeclaration.Print((VariableDeclarationSyntax)syntaxNode, context),
                SyntaxKind.VariableDeclarator => VariableDeclarator.Print((VariableDeclaratorSyntax)syntaxNode, context),
                SyntaxKind.VarPattern => VarPattern.Print((VarPatternSyntax)syntaxNode, context),
                SyntaxKind.WhenClause => WhenClause.Print((WhenClauseSyntax)syntaxNode, context),
                SyntaxKind.WhereClause => WhereClause.Print((WhereClauseSyntax)syntaxNode, context),
                SyntaxKind.WhileStatement => WhileStatement.Print((WhileStatementSyntax)syntaxNode, context),
                SyntaxKind.WithExpression => WithExpression.Print((WithExpressionSyntax)syntaxNode, context),
                SyntaxKind.YieldReturnStatement or SyntaxKind.YieldBreakStatement => YieldStatement.Print((YieldStatementSyntax)syntaxNode, context),
                _ => UnhandledNode.Print(syntaxNode, context)
            };
        }
        finally
        {
            context.State.PrintingDepth--;
        }
    }
}