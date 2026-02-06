using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Feiyue.Formatter.CSharp;

internal static partial class SyntaxNodeJsonWriter
{
    public static void WriteSyntaxNode(StringBuilder builder, SyntaxNode syntaxNode)
    {
        if (syntaxNode is AccessorDeclarationSyntax)
            WriteAccessorDeclarationSyntax(builder, syntaxNode as AccessorDeclarationSyntax);

        if (syntaxNode is AccessorListSyntax)
            WriteAccessorListSyntax(builder, syntaxNode as AccessorListSyntax);

        if (syntaxNode is AliasQualifiedNameSyntax)
            WriteAliasQualifiedNameSyntax(builder, syntaxNode as AliasQualifiedNameSyntax);

        if (syntaxNode is AllowsConstraintClauseSyntax)
            WriteAllowsConstraintClauseSyntax(builder, syntaxNode as AllowsConstraintClauseSyntax);

        if (syntaxNode is AnonymousMethodExpressionSyntax)
            WriteAnonymousMethodExpressionSyntax(builder, syntaxNode as AnonymousMethodExpressionSyntax);

        if (syntaxNode is AnonymousObjectCreationExpressionSyntax)
            WriteAnonymousObjectCreationExpressionSyntax(builder, syntaxNode as AnonymousObjectCreationExpressionSyntax);

        if (syntaxNode is AnonymousObjectMemberDeclaratorSyntax)
            WriteAnonymousObjectMemberDeclaratorSyntax(builder, syntaxNode as AnonymousObjectMemberDeclaratorSyntax);

        if (syntaxNode is ArgumentListSyntax)
            WriteArgumentListSyntax(builder, syntaxNode as ArgumentListSyntax);

        if (syntaxNode is ArgumentSyntax)
            WriteArgumentSyntax(builder, syntaxNode as ArgumentSyntax);

        if (syntaxNode is ArrayCreationExpressionSyntax)
            WriteArrayCreationExpressionSyntax(builder, syntaxNode as ArrayCreationExpressionSyntax);

        if (syntaxNode is ArrayRankSpecifierSyntax)
            WriteArrayRankSpecifierSyntax(builder, syntaxNode as ArrayRankSpecifierSyntax);

        if (syntaxNode is ArrayTypeSyntax)
            WriteArrayTypeSyntax(builder, syntaxNode as ArrayTypeSyntax);

        if (syntaxNode is ArrowExpressionClauseSyntax)
            WriteArrowExpressionClauseSyntax(builder, syntaxNode as ArrowExpressionClauseSyntax);

        if (syntaxNode is AssignmentExpressionSyntax)
            WriteAssignmentExpressionSyntax(builder, syntaxNode as AssignmentExpressionSyntax);

        if (syntaxNode is AttributeArgumentListSyntax)
            WriteAttributeArgumentListSyntax(builder, syntaxNode as AttributeArgumentListSyntax);

        if (syntaxNode is AttributeArgumentSyntax)
            WriteAttributeArgumentSyntax(builder, syntaxNode as AttributeArgumentSyntax);

        if (syntaxNode is AttributeListSyntax)
            WriteAttributeListSyntax(builder, syntaxNode as AttributeListSyntax);

        if (syntaxNode is AttributeSyntax)
            WriteAttributeSyntax(builder, syntaxNode as AttributeSyntax);

        if (syntaxNode is AttributeTargetSpecifierSyntax)
            WriteAttributeTargetSpecifierSyntax(builder, syntaxNode as AttributeTargetSpecifierSyntax);

        if (syntaxNode is AwaitExpressionSyntax)
            WriteAwaitExpressionSyntax(builder, syntaxNode as AwaitExpressionSyntax);

        if (syntaxNode is BadDirectiveTriviaSyntax)
            WriteBadDirectiveTriviaSyntax(builder, syntaxNode as BadDirectiveTriviaSyntax);

        if (syntaxNode is BaseExpressionSyntax)
            WriteBaseExpressionSyntax(builder, syntaxNode as BaseExpressionSyntax);

        if (syntaxNode is BaseListSyntax)
            WriteBaseListSyntax(builder, syntaxNode as BaseListSyntax);

        if (syntaxNode is BinaryExpressionSyntax)
            WriteBinaryExpressionSyntax(builder, syntaxNode as BinaryExpressionSyntax);

        if (syntaxNode is BinaryPatternSyntax)
            WriteBinaryPatternSyntax(builder, syntaxNode as BinaryPatternSyntax);

        if (syntaxNode is BlockSyntax)
            WriteBlockSyntax(builder, syntaxNode as BlockSyntax);

        if (syntaxNode is BracketedArgumentListSyntax)
            WriteBracketedArgumentListSyntax(builder, syntaxNode as BracketedArgumentListSyntax);

        if (syntaxNode is BracketedParameterListSyntax)
            WriteBracketedParameterListSyntax(builder, syntaxNode as BracketedParameterListSyntax);

        if (syntaxNode is BreakStatementSyntax)
            WriteBreakStatementSyntax(builder, syntaxNode as BreakStatementSyntax);

        if (syntaxNode is CasePatternSwitchLabelSyntax)
            WriteCasePatternSwitchLabelSyntax(builder, syntaxNode as CasePatternSwitchLabelSyntax);

        if (syntaxNode is CaseSwitchLabelSyntax)
            WriteCaseSwitchLabelSyntax(builder, syntaxNode as CaseSwitchLabelSyntax);

        if (syntaxNode is CastExpressionSyntax)
            WriteCastExpressionSyntax(builder, syntaxNode as CastExpressionSyntax);

        if (syntaxNode is CatchClauseSyntax)
            WriteCatchClauseSyntax(builder, syntaxNode as CatchClauseSyntax);

        if (syntaxNode is CatchDeclarationSyntax)
            WriteCatchDeclarationSyntax(builder, syntaxNode as CatchDeclarationSyntax);

        if (syntaxNode is CatchFilterClauseSyntax)
            WriteCatchFilterClauseSyntax(builder, syntaxNode as CatchFilterClauseSyntax);

        if (syntaxNode is CheckedExpressionSyntax)
            WriteCheckedExpressionSyntax(builder, syntaxNode as CheckedExpressionSyntax);

        if (syntaxNode is CheckedStatementSyntax)
            WriteCheckedStatementSyntax(builder, syntaxNode as CheckedStatementSyntax);

        if (syntaxNode is ClassDeclarationSyntax)
            WriteClassDeclarationSyntax(builder, syntaxNode as ClassDeclarationSyntax);

        if (syntaxNode is ClassOrStructConstraintSyntax)
            WriteClassOrStructConstraintSyntax(builder, syntaxNode as ClassOrStructConstraintSyntax);

        if (syntaxNode is CollectionExpressionSyntax)
            WriteCollectionExpressionSyntax(builder, syntaxNode as CollectionExpressionSyntax);

        if (syntaxNode is CompilationUnitSyntax)
            WriteCompilationUnitSyntax(builder, syntaxNode as CompilationUnitSyntax);

        if (syntaxNode is ConditionalAccessExpressionSyntax)
            WriteConditionalAccessExpressionSyntax(builder, syntaxNode as ConditionalAccessExpressionSyntax);

        if (syntaxNode is ConditionalExpressionSyntax)
            WriteConditionalExpressionSyntax(builder, syntaxNode as ConditionalExpressionSyntax);

        if (syntaxNode is ConstantPatternSyntax)
            WriteConstantPatternSyntax(builder, syntaxNode as ConstantPatternSyntax);

        if (syntaxNode is ConstructorConstraintSyntax)
            WriteConstructorConstraintSyntax(builder, syntaxNode as ConstructorConstraintSyntax);

        if (syntaxNode is ConstructorDeclarationSyntax)
            WriteConstructorDeclarationSyntax(builder, syntaxNode as ConstructorDeclarationSyntax);

        if (syntaxNode is ConstructorInitializerSyntax)
            WriteConstructorInitializerSyntax(builder, syntaxNode as ConstructorInitializerSyntax);

        if (syntaxNode is ContinueStatementSyntax)
            WriteContinueStatementSyntax(builder, syntaxNode as ContinueStatementSyntax);

        if (syntaxNode is ConversionOperatorDeclarationSyntax)
            WriteConversionOperatorDeclarationSyntax(builder, syntaxNode as ConversionOperatorDeclarationSyntax);

        if (syntaxNode is ConversionOperatorMemberCrefSyntax)
            WriteConversionOperatorMemberCrefSyntax(builder, syntaxNode as ConversionOperatorMemberCrefSyntax);

        if (syntaxNode is CrefBracketedParameterListSyntax)
            WriteCrefBracketedParameterListSyntax(builder, syntaxNode as CrefBracketedParameterListSyntax);

        if (syntaxNode is CrefParameterListSyntax)
            WriteCrefParameterListSyntax(builder, syntaxNode as CrefParameterListSyntax);

        if (syntaxNode is CrefParameterSyntax)
            WriteCrefParameterSyntax(builder, syntaxNode as CrefParameterSyntax);

        if (syntaxNode is DeclarationExpressionSyntax)
            WriteDeclarationExpressionSyntax(builder, syntaxNode as DeclarationExpressionSyntax);

        if (syntaxNode is DeclarationPatternSyntax)
            WriteDeclarationPatternSyntax(builder, syntaxNode as DeclarationPatternSyntax);

        if (syntaxNode is DefaultConstraintSyntax)
            WriteDefaultConstraintSyntax(builder, syntaxNode as DefaultConstraintSyntax);

        if (syntaxNode is DefaultExpressionSyntax)
            WriteDefaultExpressionSyntax(builder, syntaxNode as DefaultExpressionSyntax);

        if (syntaxNode is DefaultSwitchLabelSyntax)
            WriteDefaultSwitchLabelSyntax(builder, syntaxNode as DefaultSwitchLabelSyntax);

        if (syntaxNode is DefineDirectiveTriviaSyntax)
            WriteDefineDirectiveTriviaSyntax(builder, syntaxNode as DefineDirectiveTriviaSyntax);

        if (syntaxNode is DelegateDeclarationSyntax)
            WriteDelegateDeclarationSyntax(builder, syntaxNode as DelegateDeclarationSyntax);

        if (syntaxNode is DestructorDeclarationSyntax)
            WriteDestructorDeclarationSyntax(builder, syntaxNode as DestructorDeclarationSyntax);

        if (syntaxNode is DiscardDesignationSyntax)
            WriteDiscardDesignationSyntax(builder, syntaxNode as DiscardDesignationSyntax);

        if (syntaxNode is DiscardPatternSyntax)
            WriteDiscardPatternSyntax(builder, syntaxNode as DiscardPatternSyntax);

        if (syntaxNode is DocumentationCommentTriviaSyntax)
            WriteDocumentationCommentTriviaSyntax(builder, syntaxNode as DocumentationCommentTriviaSyntax);

        if (syntaxNode is DoStatementSyntax)
            WriteDoStatementSyntax(builder, syntaxNode as DoStatementSyntax);

        if (syntaxNode is ElementAccessExpressionSyntax)
            WriteElementAccessExpressionSyntax(builder, syntaxNode as ElementAccessExpressionSyntax);

        if (syntaxNode is ElementBindingExpressionSyntax)
            WriteElementBindingExpressionSyntax(builder, syntaxNode as ElementBindingExpressionSyntax);

        if (syntaxNode is ElifDirectiveTriviaSyntax)
            WriteElifDirectiveTriviaSyntax(builder, syntaxNode as ElifDirectiveTriviaSyntax);

        if (syntaxNode is ElseClauseSyntax)
            WriteElseClauseSyntax(builder, syntaxNode as ElseClauseSyntax);

        if (syntaxNode is ElseDirectiveTriviaSyntax)
            WriteElseDirectiveTriviaSyntax(builder, syntaxNode as ElseDirectiveTriviaSyntax);

        if (syntaxNode is EmptyStatementSyntax)
            WriteEmptyStatementSyntax(builder, syntaxNode as EmptyStatementSyntax);

        if (syntaxNode is EndIfDirectiveTriviaSyntax)
            WriteEndIfDirectiveTriviaSyntax(builder, syntaxNode as EndIfDirectiveTriviaSyntax);

        if (syntaxNode is EndRegionDirectiveTriviaSyntax)
            WriteEndRegionDirectiveTriviaSyntax(builder, syntaxNode as EndRegionDirectiveTriviaSyntax);

        if (syntaxNode is EnumDeclarationSyntax)
            WriteEnumDeclarationSyntax(builder, syntaxNode as EnumDeclarationSyntax);

        if (syntaxNode is EnumMemberDeclarationSyntax)
            WriteEnumMemberDeclarationSyntax(builder, syntaxNode as EnumMemberDeclarationSyntax);

        if (syntaxNode is EqualsValueClauseSyntax)
            WriteEqualsValueClauseSyntax(builder, syntaxNode as EqualsValueClauseSyntax);

        if (syntaxNode is ErrorDirectiveTriviaSyntax)
            WriteErrorDirectiveTriviaSyntax(builder, syntaxNode as ErrorDirectiveTriviaSyntax);

        if (syntaxNode is EventDeclarationSyntax)
            WriteEventDeclarationSyntax(builder, syntaxNode as EventDeclarationSyntax);

        if (syntaxNode is EventFieldDeclarationSyntax)
            WriteEventFieldDeclarationSyntax(builder, syntaxNode as EventFieldDeclarationSyntax);

        if (syntaxNode is ExplicitInterfaceSpecifierSyntax)
            WriteExplicitInterfaceSpecifierSyntax(builder, syntaxNode as ExplicitInterfaceSpecifierSyntax);

        if (syntaxNode is ExpressionColonSyntax)
            WriteExpressionColonSyntax(builder, syntaxNode as ExpressionColonSyntax);

        if (syntaxNode is ExpressionElementSyntax)
            WriteExpressionElementSyntax(builder, syntaxNode as ExpressionElementSyntax);

        if (syntaxNode is ExpressionStatementSyntax)
            WriteExpressionStatementSyntax(builder, syntaxNode as ExpressionStatementSyntax);

        if (syntaxNode is ExternAliasDirectiveSyntax)
            WriteExternAliasDirectiveSyntax(builder, syntaxNode as ExternAliasDirectiveSyntax);

        if (syntaxNode is FieldDeclarationSyntax)
            WriteFieldDeclarationSyntax(builder, syntaxNode as FieldDeclarationSyntax);

        if (syntaxNode is FieldExpressionSyntax)
            WriteFieldExpressionSyntax(builder, syntaxNode as FieldExpressionSyntax);

        if (syntaxNode is FileScopedNamespaceDeclarationSyntax)
            WriteFileScopedNamespaceDeclarationSyntax(builder, syntaxNode as FileScopedNamespaceDeclarationSyntax);

        if (syntaxNode is FinallyClauseSyntax)
            WriteFinallyClauseSyntax(builder, syntaxNode as FinallyClauseSyntax);

        if (syntaxNode is FixedStatementSyntax)
            WriteFixedStatementSyntax(builder, syntaxNode as FixedStatementSyntax);

        if (syntaxNode is ForEachStatementSyntax)
            WriteForEachStatementSyntax(builder, syntaxNode as ForEachStatementSyntax);

        if (syntaxNode is ForEachVariableStatementSyntax)
            WriteForEachVariableStatementSyntax(builder, syntaxNode as ForEachVariableStatementSyntax);

        if (syntaxNode is ForStatementSyntax)
            WriteForStatementSyntax(builder, syntaxNode as ForStatementSyntax);

        if (syntaxNode is FromClauseSyntax)
            WriteFromClauseSyntax(builder, syntaxNode as FromClauseSyntax);

        if (syntaxNode is FunctionPointerCallingConventionSyntax)
            WriteFunctionPointerCallingConventionSyntax(builder, syntaxNode as FunctionPointerCallingConventionSyntax);

        if (syntaxNode is FunctionPointerParameterListSyntax)
            WriteFunctionPointerParameterListSyntax(builder, syntaxNode as FunctionPointerParameterListSyntax);

        if (syntaxNode is FunctionPointerParameterSyntax)
            WriteFunctionPointerParameterSyntax(builder, syntaxNode as FunctionPointerParameterSyntax);

        if (syntaxNode is FunctionPointerTypeSyntax)
            WriteFunctionPointerTypeSyntax(builder, syntaxNode as FunctionPointerTypeSyntax);

        if (syntaxNode is FunctionPointerUnmanagedCallingConventionListSyntax)
            WriteFunctionPointerUnmanagedCallingConventionListSyntax(builder, syntaxNode as FunctionPointerUnmanagedCallingConventionListSyntax);

        if (syntaxNode is FunctionPointerUnmanagedCallingConventionSyntax)
            WriteFunctionPointerUnmanagedCallingConventionSyntax(builder, syntaxNode as FunctionPointerUnmanagedCallingConventionSyntax);

        if (syntaxNode is GenericNameSyntax)
            WriteGenericNameSyntax(builder, syntaxNode as GenericNameSyntax);

        if (syntaxNode is GlobalStatementSyntax)
            WriteGlobalStatementSyntax(builder, syntaxNode as GlobalStatementSyntax);

        if (syntaxNode is GotoStatementSyntax)
            WriteGotoStatementSyntax(builder, syntaxNode as GotoStatementSyntax);

        if (syntaxNode is GroupClauseSyntax)
            WriteGroupClauseSyntax(builder, syntaxNode as GroupClauseSyntax);

        if (syntaxNode is IdentifierNameSyntax)
            WriteIdentifierNameSyntax(builder, syntaxNode as IdentifierNameSyntax);

        if (syntaxNode is IfDirectiveTriviaSyntax)
            WriteIfDirectiveTriviaSyntax(builder, syntaxNode as IfDirectiveTriviaSyntax);

        if (syntaxNode is IfStatementSyntax)
            WriteIfStatementSyntax(builder, syntaxNode as IfStatementSyntax);

        if (syntaxNode is IgnoredDirectiveTriviaSyntax)
            WriteIgnoredDirectiveTriviaSyntax(builder, syntaxNode as IgnoredDirectiveTriviaSyntax);

        if (syntaxNode is ImplicitArrayCreationExpressionSyntax)
            WriteImplicitArrayCreationExpressionSyntax(builder, syntaxNode as ImplicitArrayCreationExpressionSyntax);

        if (syntaxNode is ImplicitElementAccessSyntax)
            WriteImplicitElementAccessSyntax(builder, syntaxNode as ImplicitElementAccessSyntax);

        if (syntaxNode is ImplicitObjectCreationExpressionSyntax)
            WriteImplicitObjectCreationExpressionSyntax(builder, syntaxNode as ImplicitObjectCreationExpressionSyntax);

        if (syntaxNode is ImplicitStackAllocArrayCreationExpressionSyntax)
            WriteImplicitStackAllocArrayCreationExpressionSyntax(builder, syntaxNode as ImplicitStackAllocArrayCreationExpressionSyntax);

        if (syntaxNode is IncompleteMemberSyntax)
            WriteIncompleteMemberSyntax(builder, syntaxNode as IncompleteMemberSyntax);

        if (syntaxNode is IndexerDeclarationSyntax)
            WriteIndexerDeclarationSyntax(builder, syntaxNode as IndexerDeclarationSyntax);

        if (syntaxNode is IndexerMemberCrefSyntax)
            WriteIndexerMemberCrefSyntax(builder, syntaxNode as IndexerMemberCrefSyntax);

        if (syntaxNode is InitializerExpressionSyntax)
            WriteInitializerExpressionSyntax(builder, syntaxNode as InitializerExpressionSyntax);

        if (syntaxNode is InterfaceDeclarationSyntax)
            WriteInterfaceDeclarationSyntax(builder, syntaxNode as InterfaceDeclarationSyntax);

        if (syntaxNode is InterpolatedStringExpressionSyntax)
            WriteInterpolatedStringExpressionSyntax(builder, syntaxNode as InterpolatedStringExpressionSyntax);

        if (syntaxNode is InterpolatedStringTextSyntax)
            WriteInterpolatedStringTextSyntax(builder, syntaxNode as InterpolatedStringTextSyntax);

        if (syntaxNode is InterpolationAlignmentClauseSyntax)
            WriteInterpolationAlignmentClauseSyntax(builder, syntaxNode as InterpolationAlignmentClauseSyntax);

        if (syntaxNode is InterpolationFormatClauseSyntax)
            WriteInterpolationFormatClauseSyntax(builder, syntaxNode as InterpolationFormatClauseSyntax);

        if (syntaxNode is InterpolationSyntax)
            WriteInterpolationSyntax(builder, syntaxNode as InterpolationSyntax);

        if (syntaxNode is InvocationExpressionSyntax)
            WriteInvocationExpressionSyntax(builder, syntaxNode as InvocationExpressionSyntax);

        if (syntaxNode is IsPatternExpressionSyntax)
            WriteIsPatternExpressionSyntax(builder, syntaxNode as IsPatternExpressionSyntax);

        if (syntaxNode is JoinClauseSyntax)
            WriteJoinClauseSyntax(builder, syntaxNode as JoinClauseSyntax);

        if (syntaxNode is JoinIntoClauseSyntax)
            WriteJoinIntoClauseSyntax(builder, syntaxNode as JoinIntoClauseSyntax);

        if (syntaxNode is LabeledStatementSyntax)
            WriteLabeledStatementSyntax(builder, syntaxNode as LabeledStatementSyntax);

        if (syntaxNode is LetClauseSyntax)
            WriteLetClauseSyntax(builder, syntaxNode as LetClauseSyntax);

        if (syntaxNode is LineDirectivePositionSyntax)
            WriteLineDirectivePositionSyntax(builder, syntaxNode as LineDirectivePositionSyntax);

        if (syntaxNode is LineDirectiveTriviaSyntax)
            WriteLineDirectiveTriviaSyntax(builder, syntaxNode as LineDirectiveTriviaSyntax);

        if (syntaxNode is LineSpanDirectiveTriviaSyntax)
            WriteLineSpanDirectiveTriviaSyntax(builder, syntaxNode as LineSpanDirectiveTriviaSyntax);

        if (syntaxNode is ListPatternSyntax)
            WriteListPatternSyntax(builder, syntaxNode as ListPatternSyntax);

        if (syntaxNode is LiteralExpressionSyntax)
            WriteLiteralExpressionSyntax(builder, syntaxNode as LiteralExpressionSyntax);

        if (syntaxNode is LoadDirectiveTriviaSyntax)
            WriteLoadDirectiveTriviaSyntax(builder, syntaxNode as LoadDirectiveTriviaSyntax);

        if (syntaxNode is LocalDeclarationStatementSyntax)
            WriteLocalDeclarationStatementSyntax(builder, syntaxNode as LocalDeclarationStatementSyntax);

        if (syntaxNode is LocalFunctionStatementSyntax)
            WriteLocalFunctionStatementSyntax(builder, syntaxNode as LocalFunctionStatementSyntax);

        if (syntaxNode is LockStatementSyntax)
            WriteLockStatementSyntax(builder, syntaxNode as LockStatementSyntax);

        if (syntaxNode is MakeRefExpressionSyntax)
            WriteMakeRefExpressionSyntax(builder, syntaxNode as MakeRefExpressionSyntax);

        if (syntaxNode is MemberAccessExpressionSyntax)
            WriteMemberAccessExpressionSyntax(builder, syntaxNode as MemberAccessExpressionSyntax);

        if (syntaxNode is MemberBindingExpressionSyntax)
            WriteMemberBindingExpressionSyntax(builder, syntaxNode as MemberBindingExpressionSyntax);

        if (syntaxNode is MethodDeclarationSyntax)
            WriteMethodDeclarationSyntax(builder, syntaxNode as MethodDeclarationSyntax);

        if (syntaxNode is NameColonSyntax)
            WriteNameColonSyntax(builder, syntaxNode as NameColonSyntax);

        if (syntaxNode is NameEqualsSyntax)
            WriteNameEqualsSyntax(builder, syntaxNode as NameEqualsSyntax);

        if (syntaxNode is NameMemberCrefSyntax)
            WriteNameMemberCrefSyntax(builder, syntaxNode as NameMemberCrefSyntax);

        if (syntaxNode is NamespaceDeclarationSyntax)
            WriteNamespaceDeclarationSyntax(builder, syntaxNode as NamespaceDeclarationSyntax);

        if (syntaxNode is NullableDirectiveTriviaSyntax)
            WriteNullableDirectiveTriviaSyntax(builder, syntaxNode as NullableDirectiveTriviaSyntax);

        if (syntaxNode is NullableTypeSyntax)
            WriteNullableTypeSyntax(builder, syntaxNode as NullableTypeSyntax);

        if (syntaxNode is ObjectCreationExpressionSyntax)
            WriteObjectCreationExpressionSyntax(builder, syntaxNode as ObjectCreationExpressionSyntax);

        if (syntaxNode is OmittedArraySizeExpressionSyntax)
            WriteOmittedArraySizeExpressionSyntax(builder, syntaxNode as OmittedArraySizeExpressionSyntax);

        if (syntaxNode is OmittedTypeArgumentSyntax)
            WriteOmittedTypeArgumentSyntax(builder, syntaxNode as OmittedTypeArgumentSyntax);

        if (syntaxNode is OperatorDeclarationSyntax)
            WriteOperatorDeclarationSyntax(builder, syntaxNode as OperatorDeclarationSyntax);

        if (syntaxNode is OperatorMemberCrefSyntax)
            WriteOperatorMemberCrefSyntax(builder, syntaxNode as OperatorMemberCrefSyntax);

        if (syntaxNode is OrderByClauseSyntax)
            WriteOrderByClauseSyntax(builder, syntaxNode as OrderByClauseSyntax);

        if (syntaxNode is OrderingSyntax)
            WriteOrderingSyntax(builder, syntaxNode as OrderingSyntax);

        if (syntaxNode is ParameterListSyntax)
            WriteParameterListSyntax(builder, syntaxNode as ParameterListSyntax);

        if (syntaxNode is ParameterSyntax)
            WriteParameterSyntax(builder, syntaxNode as ParameterSyntax);

        if (syntaxNode is ParenthesizedExpressionSyntax)
            WriteParenthesizedExpressionSyntax(builder, syntaxNode as ParenthesizedExpressionSyntax);

        if (syntaxNode is ParenthesizedLambdaExpressionSyntax)
            WriteParenthesizedLambdaExpressionSyntax(builder, syntaxNode as ParenthesizedLambdaExpressionSyntax);

        if (syntaxNode is ParenthesizedPatternSyntax)
            WriteParenthesizedPatternSyntax(builder, syntaxNode as ParenthesizedPatternSyntax);

        if (syntaxNode is ParenthesizedVariableDesignationSyntax)
            WriteParenthesizedVariableDesignationSyntax(builder, syntaxNode as ParenthesizedVariableDesignationSyntax);

        if (syntaxNode is PointerTypeSyntax)
            WritePointerTypeSyntax(builder, syntaxNode as PointerTypeSyntax);

        if (syntaxNode is PositionalPatternClauseSyntax)
            WritePositionalPatternClauseSyntax(builder, syntaxNode as PositionalPatternClauseSyntax);

        if (syntaxNode is PostfixUnaryExpressionSyntax)
            WritePostfixUnaryExpressionSyntax(builder, syntaxNode as PostfixUnaryExpressionSyntax);

        if (syntaxNode is PragmaChecksumDirectiveTriviaSyntax)
            WritePragmaChecksumDirectiveTriviaSyntax(builder, syntaxNode as PragmaChecksumDirectiveTriviaSyntax);

        if (syntaxNode is PragmaWarningDirectiveTriviaSyntax)
            WritePragmaWarningDirectiveTriviaSyntax(builder, syntaxNode as PragmaWarningDirectiveTriviaSyntax);

        if (syntaxNode is PredefinedTypeSyntax)
            WritePredefinedTypeSyntax(builder, syntaxNode as PredefinedTypeSyntax);

        if (syntaxNode is PrefixUnaryExpressionSyntax)
            WritePrefixUnaryExpressionSyntax(builder, syntaxNode as PrefixUnaryExpressionSyntax);

        if (syntaxNode is PrimaryConstructorBaseTypeSyntax)
            WritePrimaryConstructorBaseTypeSyntax(builder, syntaxNode as PrimaryConstructorBaseTypeSyntax);

        if (syntaxNode is PropertyDeclarationSyntax)
            WritePropertyDeclarationSyntax(builder, syntaxNode as PropertyDeclarationSyntax);

        if (syntaxNode is PropertyPatternClauseSyntax)
            WritePropertyPatternClauseSyntax(builder, syntaxNode as PropertyPatternClauseSyntax);

        if (syntaxNode is QualifiedCrefSyntax)
            WriteQualifiedCrefSyntax(builder, syntaxNode as QualifiedCrefSyntax);

        if (syntaxNode is QualifiedNameSyntax)
            WriteQualifiedNameSyntax(builder, syntaxNode as QualifiedNameSyntax);

        if (syntaxNode is QueryBodySyntax)
            WriteQueryBodySyntax(builder, syntaxNode as QueryBodySyntax);

        if (syntaxNode is QueryContinuationSyntax)
            WriteQueryContinuationSyntax(builder, syntaxNode as QueryContinuationSyntax);

        if (syntaxNode is QueryExpressionSyntax)
            WriteQueryExpressionSyntax(builder, syntaxNode as QueryExpressionSyntax);

        if (syntaxNode is RangeExpressionSyntax)
            WriteRangeExpressionSyntax(builder, syntaxNode as RangeExpressionSyntax);

        if (syntaxNode is RecordDeclarationSyntax)
            WriteRecordDeclarationSyntax(builder, syntaxNode as RecordDeclarationSyntax);

        if (syntaxNode is RecursivePatternSyntax)
            WriteRecursivePatternSyntax(builder, syntaxNode as RecursivePatternSyntax);

        if (syntaxNode is ReferenceDirectiveTriviaSyntax)
            WriteReferenceDirectiveTriviaSyntax(builder, syntaxNode as ReferenceDirectiveTriviaSyntax);

        if (syntaxNode is RefExpressionSyntax)
            WriteRefExpressionSyntax(builder, syntaxNode as RefExpressionSyntax);

        if (syntaxNode is RefStructConstraintSyntax)
            WriteRefStructConstraintSyntax(builder, syntaxNode as RefStructConstraintSyntax);

        if (syntaxNode is RefTypeExpressionSyntax)
            WriteRefTypeExpressionSyntax(builder, syntaxNode as RefTypeExpressionSyntax);

        if (syntaxNode is RefTypeSyntax)
            WriteRefTypeSyntax(builder, syntaxNode as RefTypeSyntax);

        if (syntaxNode is RefValueExpressionSyntax)
            WriteRefValueExpressionSyntax(builder, syntaxNode as RefValueExpressionSyntax);

        if (syntaxNode is RegionDirectiveTriviaSyntax)
            WriteRegionDirectiveTriviaSyntax(builder, syntaxNode as RegionDirectiveTriviaSyntax);

        if (syntaxNode is RelationalPatternSyntax)
            WriteRelationalPatternSyntax(builder, syntaxNode as RelationalPatternSyntax);

        if (syntaxNode is ReturnStatementSyntax)
            WriteReturnStatementSyntax(builder, syntaxNode as ReturnStatementSyntax);

        if (syntaxNode is ScopedTypeSyntax)
            WriteScopedTypeSyntax(builder, syntaxNode as ScopedTypeSyntax);

        if (syntaxNode is SelectClauseSyntax)
            WriteSelectClauseSyntax(builder, syntaxNode as SelectClauseSyntax);

        if (syntaxNode is ShebangDirectiveTriviaSyntax)
            WriteShebangDirectiveTriviaSyntax(builder, syntaxNode as ShebangDirectiveTriviaSyntax);

        if (syntaxNode is SimpleBaseTypeSyntax)
            WriteSimpleBaseTypeSyntax(builder, syntaxNode as SimpleBaseTypeSyntax);

        if (syntaxNode is SimpleLambdaExpressionSyntax)
            WriteSimpleLambdaExpressionSyntax(builder, syntaxNode as SimpleLambdaExpressionSyntax);

        if (syntaxNode is SingleVariableDesignationSyntax)
            WriteSingleVariableDesignationSyntax(builder, syntaxNode as SingleVariableDesignationSyntax);

        if (syntaxNode is SizeOfExpressionSyntax)
            WriteSizeOfExpressionSyntax(builder, syntaxNode as SizeOfExpressionSyntax);

        if (syntaxNode is SkippedTokensTriviaSyntax)
            WriteSkippedTokensTriviaSyntax(builder, syntaxNode as SkippedTokensTriviaSyntax);

        if (syntaxNode is SlicePatternSyntax)
            WriteSlicePatternSyntax(builder, syntaxNode as SlicePatternSyntax);

        if (syntaxNode is SpreadElementSyntax)
            WriteSpreadElementSyntax(builder, syntaxNode as SpreadElementSyntax);

        if (syntaxNode is StackAllocArrayCreationExpressionSyntax)
            WriteStackAllocArrayCreationExpressionSyntax(builder, syntaxNode as StackAllocArrayCreationExpressionSyntax);

        if (syntaxNode is StructDeclarationSyntax)
            WriteStructDeclarationSyntax(builder, syntaxNode as StructDeclarationSyntax);

        if (syntaxNode is SubpatternSyntax)
            WriteSubpatternSyntax(builder, syntaxNode as SubpatternSyntax);

        if (syntaxNode is SwitchExpressionArmSyntax)
            WriteSwitchExpressionArmSyntax(builder, syntaxNode as SwitchExpressionArmSyntax);

        if (syntaxNode is SwitchExpressionSyntax)
            WriteSwitchExpressionSyntax(builder, syntaxNode as SwitchExpressionSyntax);

        if (syntaxNode is SwitchSectionSyntax)
            WriteSwitchSectionSyntax(builder, syntaxNode as SwitchSectionSyntax);

        if (syntaxNode is SwitchStatementSyntax)
            WriteSwitchStatementSyntax(builder, syntaxNode as SwitchStatementSyntax);

        if (syntaxNode is ThisExpressionSyntax)
            WriteThisExpressionSyntax(builder, syntaxNode as ThisExpressionSyntax);

        if (syntaxNode is ThrowExpressionSyntax)
            WriteThrowExpressionSyntax(builder, syntaxNode as ThrowExpressionSyntax);

        if (syntaxNode is ThrowStatementSyntax)
            WriteThrowStatementSyntax(builder, syntaxNode as ThrowStatementSyntax);

        if (syntaxNode is TryStatementSyntax)
            WriteTryStatementSyntax(builder, syntaxNode as TryStatementSyntax);

        if (syntaxNode is TupleElementSyntax)
            WriteTupleElementSyntax(builder, syntaxNode as TupleElementSyntax);

        if (syntaxNode is TupleExpressionSyntax)
            WriteTupleExpressionSyntax(builder, syntaxNode as TupleExpressionSyntax);

        if (syntaxNode is TupleTypeSyntax)
            WriteTupleTypeSyntax(builder, syntaxNode as TupleTypeSyntax);

        if (syntaxNode is TypeArgumentListSyntax)
            WriteTypeArgumentListSyntax(builder, syntaxNode as TypeArgumentListSyntax);

        if (syntaxNode is TypeConstraintSyntax)
            WriteTypeConstraintSyntax(builder, syntaxNode as TypeConstraintSyntax);

        if (syntaxNode is TypeCrefSyntax)
            WriteTypeCrefSyntax(builder, syntaxNode as TypeCrefSyntax);

        if (syntaxNode is TypeOfExpressionSyntax)
            WriteTypeOfExpressionSyntax(builder, syntaxNode as TypeOfExpressionSyntax);

        if (syntaxNode is TypeParameterConstraintClauseSyntax)
            WriteTypeParameterConstraintClauseSyntax(builder, syntaxNode as TypeParameterConstraintClauseSyntax);

        if (syntaxNode is TypeParameterListSyntax)
            WriteTypeParameterListSyntax(builder, syntaxNode as TypeParameterListSyntax);

        if (syntaxNode is TypeParameterSyntax)
            WriteTypeParameterSyntax(builder, syntaxNode as TypeParameterSyntax);

        if (syntaxNode is TypePatternSyntax)
            WriteTypePatternSyntax(builder, syntaxNode as TypePatternSyntax);

        if (syntaxNode is UnaryPatternSyntax)
            WriteUnaryPatternSyntax(builder, syntaxNode as UnaryPatternSyntax);

        if (syntaxNode is UndefDirectiveTriviaSyntax)
            WriteUndefDirectiveTriviaSyntax(builder, syntaxNode as UndefDirectiveTriviaSyntax);

        if (syntaxNode is UnsafeStatementSyntax)
            WriteUnsafeStatementSyntax(builder, syntaxNode as UnsafeStatementSyntax);

        if (syntaxNode is UsingDirectiveSyntax)
            WriteUsingDirectiveSyntax(builder, syntaxNode as UsingDirectiveSyntax);

        if (syntaxNode is UsingStatementSyntax)
            WriteUsingStatementSyntax(builder, syntaxNode as UsingStatementSyntax);

        if (syntaxNode is VariableDeclarationSyntax)
            WriteVariableDeclarationSyntax(builder, syntaxNode as VariableDeclarationSyntax);

        if (syntaxNode is VariableDeclaratorSyntax)
            WriteVariableDeclaratorSyntax(builder, syntaxNode as VariableDeclaratorSyntax);

        if (syntaxNode is VarPatternSyntax)
            WriteVarPatternSyntax(builder, syntaxNode as VarPatternSyntax);

        if (syntaxNode is WarningDirectiveTriviaSyntax)
            WriteWarningDirectiveTriviaSyntax(builder, syntaxNode as WarningDirectiveTriviaSyntax);

        if (syntaxNode is WhenClauseSyntax)
            WriteWhenClauseSyntax(builder, syntaxNode as WhenClauseSyntax);

        if (syntaxNode is WhereClauseSyntax)
            WriteWhereClauseSyntax(builder, syntaxNode as WhereClauseSyntax);

        if (syntaxNode is WhileStatementSyntax)
            WriteWhileStatementSyntax(builder, syntaxNode as WhileStatementSyntax);

        if (syntaxNode is WithExpressionSyntax)
            WriteWithExpressionSyntax(builder, syntaxNode as WithExpressionSyntax);

        if (syntaxNode is XmlCDataSectionSyntax)
            WriteXmlCDataSectionSyntax(builder, syntaxNode as XmlCDataSectionSyntax);

        if (syntaxNode is XmlCommentSyntax)
            WriteXmlCommentSyntax(builder, syntaxNode as XmlCommentSyntax);

        if (syntaxNode is XmlCrefAttributeSyntax)
            WriteXmlCrefAttributeSyntax(builder, syntaxNode as XmlCrefAttributeSyntax);

        if (syntaxNode is XmlElementEndTagSyntax)
            WriteXmlElementEndTagSyntax(builder, syntaxNode as XmlElementEndTagSyntax);

        if (syntaxNode is XmlElementStartTagSyntax)
            WriteXmlElementStartTagSyntax(builder, syntaxNode as XmlElementStartTagSyntax);

        if (syntaxNode is XmlElementSyntax)
            WriteXmlElementSyntax(builder, syntaxNode as XmlElementSyntax);

        if (syntaxNode is XmlEmptyElementSyntax)
            WriteXmlEmptyElementSyntax(builder, syntaxNode as XmlEmptyElementSyntax);

        if (syntaxNode is XmlNameAttributeSyntax)
            WriteXmlNameAttributeSyntax(builder, syntaxNode as XmlNameAttributeSyntax);

        if (syntaxNode is XmlNameSyntax)
            WriteXmlNameSyntax(builder, syntaxNode as XmlNameSyntax);

        if (syntaxNode is XmlPrefixSyntax)
            WriteXmlPrefixSyntax(builder, syntaxNode as XmlPrefixSyntax);

        if (syntaxNode is XmlProcessingInstructionSyntax)
            WriteXmlProcessingInstructionSyntax(builder, syntaxNode as XmlProcessingInstructionSyntax);

        if (syntaxNode is XmlTextAttributeSyntax)
            WriteXmlTextAttributeSyntax(builder, syntaxNode as XmlTextAttributeSyntax);

        if (syntaxNode is XmlTextSyntax)
            WriteXmlTextSyntax(builder, syntaxNode as XmlTextSyntax);

        if (syntaxNode is YieldStatementSyntax)
            WriteYieldStatementSyntax(builder, syntaxNode as YieldStatementSyntax);
    }

    public static void WriteAccessorDeclarationSyntax(StringBuilder builder, AccessorDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Body != null)
        {
            var bodyBuilder = new StringBuilder();
            WriteBlockSyntax(bodyBuilder, syntaxNode.Body);
            properties.Add($"\"body\":{bodyBuilder}");
        }
        if (syntaxNode.ExpressionBody != null)
        {
            var expressionBodyBuilder = new StringBuilder();
            WriteArrowExpressionClauseSyntax(expressionBodyBuilder, syntaxNode.ExpressionBody);
            properties.Add($"\"expressionBody\":{expressionBodyBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteAccessorListSyntax(StringBuilder builder, AccessorListSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var accessors = new List<string>();
        foreach (var node in syntaxNode.Accessors)
        {
            var innerBuilder = new StringBuilder();
            WriteAccessorDeclarationSyntax(innerBuilder, node);
            accessors.Add(innerBuilder.ToString());
        }
        properties.Add($"\"accessors\":[{string.Join(",", accessors)}]");
        if (syntaxNode.CloseBraceToken != default)
        {
            var closeBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBraceTokenBuilder, syntaxNode.CloseBraceToken);
            properties.Add($"\"closeBraceToken\":{closeBraceTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBraceToken != default)
        {
            var openBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBraceTokenBuilder, syntaxNode.OpenBraceToken);
            properties.Add($"\"openBraceToken\":{openBraceTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteAliasQualifiedNameSyntax(StringBuilder builder, AliasQualifiedNameSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Alias != null)
        {
            var aliasBuilder = new StringBuilder();
            WriteIdentifierNameSyntax(aliasBuilder, syntaxNode.Alias);
            properties.Add($"\"alias\":{aliasBuilder}");
        }
        properties.Add(WriteInt("arity", syntaxNode.Arity));
        if (syntaxNode.ColonColonToken != default)
        {
            var colonColonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(colonColonTokenBuilder, syntaxNode.ColonColonToken);
            properties.Add($"\"colonColonToken\":{colonColonTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        properties.Add(WriteBoolean("isNint", syntaxNode.IsNint));
        properties.Add(WriteBoolean("isNotNull", syntaxNode.IsNotNull));
        properties.Add(WriteBoolean("isNuint", syntaxNode.IsNuint));
        properties.Add(WriteBoolean("isUnmanaged", syntaxNode.IsUnmanaged));
        properties.Add(WriteBoolean("isVar", syntaxNode.IsVar));
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteSyntaxNode(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteAllowsConstraintClauseSyntax(StringBuilder builder, AllowsConstraintClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.AllowsKeyword != default)
        {
            var allowsKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(allowsKeywordBuilder, syntaxNode.AllowsKeyword);
            properties.Add($"\"allowsKeyword\":{allowsKeywordBuilder}");
        }
        var constraints = new List<string>();
        foreach (var node in syntaxNode.Constraints)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            constraints.Add(innerBuilder.ToString());
        }
        properties.Add($"\"constraints\":[{string.Join(",", constraints)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteAnonymousMethodExpressionSyntax(StringBuilder builder, AnonymousMethodExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.AsyncKeyword != default)
        {
            var asyncKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(asyncKeywordBuilder, syntaxNode.AsyncKeyword);
            properties.Add($"\"asyncKeyword\":{asyncKeywordBuilder}");
        }
        if (syntaxNode.Block != null)
        {
            var blockBuilder = new StringBuilder();
            WriteBlockSyntax(blockBuilder, syntaxNode.Block);
            properties.Add($"\"block\":{blockBuilder}");
        }
        if (syntaxNode.Body != null)
        {
            var bodyBuilder = new StringBuilder();
            WriteSyntaxNode(bodyBuilder, syntaxNode.Body);
            properties.Add($"\"body\":{bodyBuilder}");
        }
        if (syntaxNode.DelegateKeyword != default)
        {
            var delegateKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(delegateKeywordBuilder, syntaxNode.DelegateKeyword);
            properties.Add($"\"delegateKeyword\":{delegateKeywordBuilder}");
        }
        if (syntaxNode.ExpressionBody != null)
        {
            var expressionBodyBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBodyBuilder, syntaxNode.ExpressionBody);
            properties.Add($"\"expressionBody\":{expressionBodyBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.ParameterList != null)
        {
            var parameterListBuilder = new StringBuilder();
            WriteParameterListSyntax(parameterListBuilder, syntaxNode.ParameterList);
            properties.Add($"\"parameterList\":{parameterListBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteAnonymousObjectCreationExpressionSyntax(StringBuilder builder, AnonymousObjectCreationExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseBraceToken != default)
        {
            var closeBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBraceTokenBuilder, syntaxNode.CloseBraceToken);
            properties.Add($"\"closeBraceToken\":{closeBraceTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        var initializers = new List<string>();
        foreach (var node in syntaxNode.Initializers)
        {
            var innerBuilder = new StringBuilder();
            WriteAnonymousObjectMemberDeclaratorSyntax(innerBuilder, node);
            initializers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"initializers\":[{string.Join(",", initializers)}]");
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.NewKeyword != default)
        {
            var newKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(newKeywordBuilder, syntaxNode.NewKeyword);
            properties.Add($"\"newKeyword\":{newKeywordBuilder}");
        }
        if (syntaxNode.OpenBraceToken != default)
        {
            var openBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBraceTokenBuilder, syntaxNode.OpenBraceToken);
            properties.Add($"\"openBraceToken\":{openBraceTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteAnonymousObjectMemberDeclaratorSyntax(StringBuilder builder, AnonymousObjectMemberDeclaratorSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.NameEquals != null)
        {
            var nameEqualsBuilder = new StringBuilder();
            WriteNameEqualsSyntax(nameEqualsBuilder, syntaxNode.NameEquals);
            properties.Add($"\"nameEquals\":{nameEqualsBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteArgumentListSyntax(StringBuilder builder, ArgumentListSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var arguments = new List<string>();
        foreach (var node in syntaxNode.Arguments)
        {
            var innerBuilder = new StringBuilder();
            WriteArgumentSyntax(innerBuilder, node);
            arguments.Add(innerBuilder.ToString());
        }
        properties.Add($"\"arguments\":[{string.Join(",", arguments)}]");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteArgumentSyntax(StringBuilder builder, ArgumentSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.NameColon != null)
        {
            var nameColonBuilder = new StringBuilder();
            WriteNameColonSyntax(nameColonBuilder, syntaxNode.NameColon);
            properties.Add($"\"nameColon\":{nameColonBuilder}");
        }
        if (syntaxNode.RefKindKeyword != default)
        {
            var refKindKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(refKindKeywordBuilder, syntaxNode.RefKindKeyword);
            properties.Add($"\"refKindKeyword\":{refKindKeywordBuilder}");
        }
        if (syntaxNode.RefOrOutKeyword != default)
        {
            var refOrOutKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(refOrOutKeywordBuilder, syntaxNode.RefOrOutKeyword);
            properties.Add($"\"refOrOutKeyword\":{refOrOutKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteArrayCreationExpressionSyntax(StringBuilder builder, ArrayCreationExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Initializer != null)
        {
            var initializerBuilder = new StringBuilder();
            WriteInitializerExpressionSyntax(initializerBuilder, syntaxNode.Initializer);
            properties.Add($"\"initializer\":{initializerBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.NewKeyword != default)
        {
            var newKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(newKeywordBuilder, syntaxNode.NewKeyword);
            properties.Add($"\"newKeyword\":{newKeywordBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteArrayTypeSyntax(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteArrayRankSpecifierSyntax(StringBuilder builder, ArrayRankSpecifierSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseBracketToken != default)
        {
            var closeBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBracketTokenBuilder, syntaxNode.CloseBracketToken);
            properties.Add($"\"closeBracketToken\":{closeBracketTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBracketToken != default)
        {
            var openBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBracketTokenBuilder, syntaxNode.OpenBracketToken);
            properties.Add($"\"openBracketToken\":{openBracketTokenBuilder}");
        }
        properties.Add(WriteInt("rank", syntaxNode.Rank));
        var sizes = new List<string>();
        foreach (var node in syntaxNode.Sizes)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            sizes.Add(innerBuilder.ToString());
        }
        properties.Add($"\"sizes\":[{string.Join(",", sizes)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteArrayTypeSyntax(StringBuilder builder, ArrayTypeSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ElementType != null)
        {
            var elementTypeBuilder = new StringBuilder();
            WriteSyntaxNode(elementTypeBuilder, syntaxNode.ElementType);
            properties.Add($"\"elementType\":{elementTypeBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        properties.Add(WriteBoolean("isNint", syntaxNode.IsNint));
        properties.Add(WriteBoolean("isNotNull", syntaxNode.IsNotNull));
        properties.Add(WriteBoolean("isNuint", syntaxNode.IsNuint));
        properties.Add(WriteBoolean("isUnmanaged", syntaxNode.IsUnmanaged));
        properties.Add(WriteBoolean("isVar", syntaxNode.IsVar));
        var rankSpecifiers = new List<string>();
        foreach (var node in syntaxNode.RankSpecifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteArrayRankSpecifierSyntax(innerBuilder, node);
            rankSpecifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"rankSpecifiers\":[{string.Join(",", rankSpecifiers)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteArrowExpressionClauseSyntax(StringBuilder builder, ArrowExpressionClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ArrowToken != default)
        {
            var arrowTokenBuilder = new StringBuilder();
            WriteSyntaxToken(arrowTokenBuilder, syntaxNode.ArrowToken);
            properties.Add($"\"arrowToken\":{arrowTokenBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteAssignmentExpressionSyntax(StringBuilder builder, AssignmentExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Left != null)
        {
            var leftBuilder = new StringBuilder();
            WriteSyntaxNode(leftBuilder, syntaxNode.Left);
            properties.Add($"\"left\":{leftBuilder}");
        }
        if (syntaxNode.OperatorToken != default)
        {
            var operatorTokenBuilder = new StringBuilder();
            WriteSyntaxToken(operatorTokenBuilder, syntaxNode.OperatorToken);
            properties.Add($"\"operatorToken\":{operatorTokenBuilder}");
        }
        if (syntaxNode.Right != null)
        {
            var rightBuilder = new StringBuilder();
            WriteSyntaxNode(rightBuilder, syntaxNode.Right);
            properties.Add($"\"right\":{rightBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteAttributeArgumentListSyntax(StringBuilder builder, AttributeArgumentListSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var arguments = new List<string>();
        foreach (var node in syntaxNode.Arguments)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeArgumentSyntax(innerBuilder, node);
            arguments.Add(innerBuilder.ToString());
        }
        properties.Add($"\"arguments\":[{string.Join(",", arguments)}]");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteAttributeArgumentSyntax(StringBuilder builder, AttributeArgumentSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.NameColon != null)
        {
            var nameColonBuilder = new StringBuilder();
            WriteNameColonSyntax(nameColonBuilder, syntaxNode.NameColon);
            properties.Add($"\"nameColon\":{nameColonBuilder}");
        }
        if (syntaxNode.NameEquals != null)
        {
            var nameEqualsBuilder = new StringBuilder();
            WriteNameEqualsSyntax(nameEqualsBuilder, syntaxNode.NameEquals);
            properties.Add($"\"nameEquals\":{nameEqualsBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteAttributeListSyntax(StringBuilder builder, AttributeListSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributes = new List<string>();
        foreach (var node in syntaxNode.Attributes)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeSyntax(innerBuilder, node);
            attributes.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributes\":[{string.Join(",", attributes)}]");
        if (syntaxNode.CloseBracketToken != default)
        {
            var closeBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBracketTokenBuilder, syntaxNode.CloseBracketToken);
            properties.Add($"\"closeBracketToken\":{closeBracketTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBracketToken != default)
        {
            var openBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBracketTokenBuilder, syntaxNode.OpenBracketToken);
            properties.Add($"\"openBracketToken\":{openBracketTokenBuilder}");
        }
        if (syntaxNode.Target != null)
        {
            var targetBuilder = new StringBuilder();
            WriteAttributeTargetSpecifierSyntax(targetBuilder, syntaxNode.Target);
            properties.Add($"\"target\":{targetBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteAttributeSyntax(StringBuilder builder, AttributeSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ArgumentList != null)
        {
            var argumentListBuilder = new StringBuilder();
            WriteAttributeArgumentListSyntax(argumentListBuilder, syntaxNode.ArgumentList);
            properties.Add($"\"argumentList\":{argumentListBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteSyntaxNode(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteAttributeTargetSpecifierSyntax(StringBuilder builder, AttributeTargetSpecifierSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ColonToken != default)
        {
            var colonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(colonTokenBuilder, syntaxNode.ColonToken);
            properties.Add($"\"colonToken\":{colonTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteAwaitExpressionSyntax(StringBuilder builder, AwaitExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.AwaitKeyword != default)
        {
            var awaitKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(awaitKeywordBuilder, syntaxNode.AwaitKeyword);
            properties.Add($"\"awaitKeyword\":{awaitKeywordBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteBadDirectiveTriviaSyntax(StringBuilder builder, BadDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteBaseExpressionSyntax(StringBuilder builder, BaseExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Token != default)
        {
            var tokenBuilder = new StringBuilder();
            WriteSyntaxToken(tokenBuilder, syntaxNode.Token);
            properties.Add($"\"token\":{tokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteBaseListSyntax(StringBuilder builder, BaseListSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ColonToken != default)
        {
            var colonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(colonTokenBuilder, syntaxNode.ColonToken);
            properties.Add($"\"colonToken\":{colonTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var types = new List<string>();
        foreach (var node in syntaxNode.Types)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            types.Add(innerBuilder.ToString());
        }
        properties.Add($"\"types\":[{string.Join(",", types)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteBinaryExpressionSyntax(StringBuilder builder, BinaryExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Left != null)
        {
            var leftBuilder = new StringBuilder();
            WriteSyntaxNode(leftBuilder, syntaxNode.Left);
            properties.Add($"\"left\":{leftBuilder}");
        }
        if (syntaxNode.OperatorToken != default)
        {
            var operatorTokenBuilder = new StringBuilder();
            WriteSyntaxToken(operatorTokenBuilder, syntaxNode.OperatorToken);
            properties.Add($"\"operatorToken\":{operatorTokenBuilder}");
        }
        if (syntaxNode.Right != null)
        {
            var rightBuilder = new StringBuilder();
            WriteSyntaxNode(rightBuilder, syntaxNode.Right);
            properties.Add($"\"right\":{rightBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteBinaryPatternSyntax(StringBuilder builder, BinaryPatternSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Left != null)
        {
            var leftBuilder = new StringBuilder();
            WriteSyntaxNode(leftBuilder, syntaxNode.Left);
            properties.Add($"\"left\":{leftBuilder}");
        }
        if (syntaxNode.OperatorToken != default)
        {
            var operatorTokenBuilder = new StringBuilder();
            WriteSyntaxToken(operatorTokenBuilder, syntaxNode.OperatorToken);
            properties.Add($"\"operatorToken\":{operatorTokenBuilder}");
        }
        if (syntaxNode.Right != null)
        {
            var rightBuilder = new StringBuilder();
            WriteSyntaxNode(rightBuilder, syntaxNode.Right);
            properties.Add($"\"right\":{rightBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteBlockSyntax(StringBuilder builder, BlockSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.CloseBraceToken != default)
        {
            var closeBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBraceTokenBuilder, syntaxNode.CloseBraceToken);
            properties.Add($"\"closeBraceToken\":{closeBraceTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBraceToken != default)
        {
            var openBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBraceTokenBuilder, syntaxNode.OpenBraceToken);
            properties.Add($"\"openBraceToken\":{openBraceTokenBuilder}");
        }
        var statements = new List<string>();
        foreach (var node in syntaxNode.Statements)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            statements.Add(innerBuilder.ToString());
        }
        properties.Add($"\"statements\":[{string.Join(",", statements)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteBracketedArgumentListSyntax(StringBuilder builder, BracketedArgumentListSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var arguments = new List<string>();
        foreach (var node in syntaxNode.Arguments)
        {
            var innerBuilder = new StringBuilder();
            WriteArgumentSyntax(innerBuilder, node);
            arguments.Add(innerBuilder.ToString());
        }
        properties.Add($"\"arguments\":[{string.Join(",", arguments)}]");
        if (syntaxNode.CloseBracketToken != default)
        {
            var closeBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBracketTokenBuilder, syntaxNode.CloseBracketToken);
            properties.Add($"\"closeBracketToken\":{closeBracketTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBracketToken != default)
        {
            var openBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBracketTokenBuilder, syntaxNode.OpenBracketToken);
            properties.Add($"\"openBracketToken\":{openBracketTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteBracketedParameterListSyntax(StringBuilder builder, BracketedParameterListSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseBracketToken != default)
        {
            var closeBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBracketTokenBuilder, syntaxNode.CloseBracketToken);
            properties.Add($"\"closeBracketToken\":{closeBracketTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBracketToken != default)
        {
            var openBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBracketTokenBuilder, syntaxNode.OpenBracketToken);
            properties.Add($"\"openBracketToken\":{openBracketTokenBuilder}");
        }
        var parameters = new List<string>();
        foreach (var node in syntaxNode.Parameters)
        {
            var innerBuilder = new StringBuilder();
            WriteParameterSyntax(innerBuilder, node);
            parameters.Add(innerBuilder.ToString());
        }
        properties.Add($"\"parameters\":[{string.Join(",", parameters)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteBreakStatementSyntax(StringBuilder builder, BreakStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.BreakKeyword != default)
        {
            var breakKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(breakKeywordBuilder, syntaxNode.BreakKeyword);
            properties.Add($"\"breakKeyword\":{breakKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteCasePatternSwitchLabelSyntax(StringBuilder builder, CasePatternSwitchLabelSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ColonToken != default)
        {
            var colonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(colonTokenBuilder, syntaxNode.ColonToken);
            properties.Add($"\"colonToken\":{colonTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        if (syntaxNode.Pattern != null)
        {
            var patternBuilder = new StringBuilder();
            WriteSyntaxNode(patternBuilder, syntaxNode.Pattern);
            properties.Add($"\"pattern\":{patternBuilder}");
        }
        if (syntaxNode.WhenClause != null)
        {
            var whenClauseBuilder = new StringBuilder();
            WriteWhenClauseSyntax(whenClauseBuilder, syntaxNode.WhenClause);
            properties.Add($"\"whenClause\":{whenClauseBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteCaseSwitchLabelSyntax(StringBuilder builder, CaseSwitchLabelSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ColonToken != default)
        {
            var colonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(colonTokenBuilder, syntaxNode.ColonToken);
            properties.Add($"\"colonToken\":{colonTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        if (syntaxNode.Value != null)
        {
            var valueBuilder = new StringBuilder();
            WriteSyntaxNode(valueBuilder, syntaxNode.Value);
            properties.Add($"\"value\":{valueBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteCastExpressionSyntax(StringBuilder builder, CastExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteCatchClauseSyntax(StringBuilder builder, CatchClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Block != null)
        {
            var blockBuilder = new StringBuilder();
            WriteBlockSyntax(blockBuilder, syntaxNode.Block);
            properties.Add($"\"block\":{blockBuilder}");
        }
        if (syntaxNode.CatchKeyword != default)
        {
            var catchKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(catchKeywordBuilder, syntaxNode.CatchKeyword);
            properties.Add($"\"catchKeyword\":{catchKeywordBuilder}");
        }
        if (syntaxNode.Declaration != null)
        {
            var declarationBuilder = new StringBuilder();
            WriteCatchDeclarationSyntax(declarationBuilder, syntaxNode.Declaration);
            properties.Add($"\"declaration\":{declarationBuilder}");
        }
        if (syntaxNode.Filter != null)
        {
            var filterBuilder = new StringBuilder();
            WriteCatchFilterClauseSyntax(filterBuilder, syntaxNode.Filter);
            properties.Add($"\"filter\":{filterBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteCatchDeclarationSyntax(StringBuilder builder, CatchDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteCatchFilterClauseSyntax(StringBuilder builder, CatchFilterClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.FilterExpression != null)
        {
            var filterExpressionBuilder = new StringBuilder();
            WriteSyntaxNode(filterExpressionBuilder, syntaxNode.FilterExpression);
            properties.Add($"\"filterExpression\":{filterExpressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.WhenKeyword != default)
        {
            var whenKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(whenKeywordBuilder, syntaxNode.WhenKeyword);
            properties.Add($"\"whenKeyword\":{whenKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteCheckedExpressionSyntax(StringBuilder builder, CheckedExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteCheckedStatementSyntax(StringBuilder builder, CheckedStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Block != null)
        {
            var blockBuilder = new StringBuilder();
            WriteBlockSyntax(blockBuilder, syntaxNode.Block);
            properties.Add($"\"block\":{blockBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteClassDeclarationSyntax(StringBuilder builder, ClassDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteInt("arity", syntaxNode.Arity));
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.BaseList != null)
        {
            var baseListBuilder = new StringBuilder();
            WriteBaseListSyntax(baseListBuilder, syntaxNode.BaseList);
            properties.Add($"\"baseList\":{baseListBuilder}");
        }
        if (syntaxNode.CloseBraceToken != default)
        {
            var closeBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBraceTokenBuilder, syntaxNode.CloseBraceToken);
            properties.Add($"\"closeBraceToken\":{closeBraceTokenBuilder}");
        }
        var constraintClauses = new List<string>();
        foreach (var node in syntaxNode.ConstraintClauses)
        {
            var innerBuilder = new StringBuilder();
            WriteTypeParameterConstraintClauseSyntax(innerBuilder, node);
            constraintClauses.Add(innerBuilder.ToString());
        }
        properties.Add($"\"constraintClauses\":[{string.Join(",", constraintClauses)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        var members = new List<string>();
        foreach (var node in syntaxNode.Members)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            members.Add(innerBuilder.ToString());
        }
        properties.Add($"\"members\":[{string.Join(",", members)}]");
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.OpenBraceToken != default)
        {
            var openBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBraceTokenBuilder, syntaxNode.OpenBraceToken);
            properties.Add($"\"openBraceToken\":{openBraceTokenBuilder}");
        }
        if (syntaxNode.ParameterList != null)
        {
            var parameterListBuilder = new StringBuilder();
            WriteParameterListSyntax(parameterListBuilder, syntaxNode.ParameterList);
            properties.Add($"\"parameterList\":{parameterListBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.TypeParameterList != null)
        {
            var typeParameterListBuilder = new StringBuilder();
            WriteTypeParameterListSyntax(typeParameterListBuilder, syntaxNode.TypeParameterList);
            properties.Add($"\"typeParameterList\":{typeParameterListBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteClassOrStructConstraintSyntax(StringBuilder builder, ClassOrStructConstraintSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ClassOrStructKeyword != default)
        {
            var classOrStructKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(classOrStructKeywordBuilder, syntaxNode.ClassOrStructKeyword);
            properties.Add($"\"classOrStructKeyword\":{classOrStructKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.QuestionToken != default)
        {
            var questionTokenBuilder = new StringBuilder();
            WriteSyntaxToken(questionTokenBuilder, syntaxNode.QuestionToken);
            properties.Add($"\"questionToken\":{questionTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteCollectionExpressionSyntax(StringBuilder builder, CollectionExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseBracketToken != default)
        {
            var closeBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBracketTokenBuilder, syntaxNode.CloseBracketToken);
            properties.Add($"\"closeBracketToken\":{closeBracketTokenBuilder}");
        }
        var elements = new List<string>();
        foreach (var node in syntaxNode.Elements)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            elements.Add(innerBuilder.ToString());
        }
        properties.Add($"\"elements\":[{string.Join(",", elements)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBracketToken != default)
        {
            var openBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBracketTokenBuilder, syntaxNode.OpenBracketToken);
            properties.Add($"\"openBracketToken\":{openBracketTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteCompilationUnitSyntax(StringBuilder builder, CompilationUnitSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.EndOfFileToken != default)
        {
            var endOfFileTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfFileTokenBuilder, syntaxNode.EndOfFileToken);
            properties.Add($"\"endOfFileToken\":{endOfFileTokenBuilder}");
        }
        var externs = new List<string>();
        foreach (var node in syntaxNode.Externs)
        {
            var innerBuilder = new StringBuilder();
            WriteExternAliasDirectiveSyntax(innerBuilder, node);
            externs.Add(innerBuilder.ToString());
        }
        properties.Add($"\"externs\":[{string.Join(",", externs)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var members = new List<string>();
        foreach (var node in syntaxNode.Members)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            members.Add(innerBuilder.ToString());
        }
        properties.Add($"\"members\":[{string.Join(",", members)}]");
        var usings = new List<string>();
        foreach (var node in syntaxNode.Usings)
        {
            var innerBuilder = new StringBuilder();
            WriteUsingDirectiveSyntax(innerBuilder, node);
            usings.Add(innerBuilder.ToString());
        }
        properties.Add($"\"usings\":[{string.Join(",", usings)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteConditionalAccessExpressionSyntax(StringBuilder builder, ConditionalAccessExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OperatorToken != default)
        {
            var operatorTokenBuilder = new StringBuilder();
            WriteSyntaxToken(operatorTokenBuilder, syntaxNode.OperatorToken);
            properties.Add($"\"operatorToken\":{operatorTokenBuilder}");
        }
        if (syntaxNode.WhenNotNull != null)
        {
            var whenNotNullBuilder = new StringBuilder();
            WriteSyntaxNode(whenNotNullBuilder, syntaxNode.WhenNotNull);
            properties.Add($"\"whenNotNull\":{whenNotNullBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteConditionalExpressionSyntax(StringBuilder builder, ConditionalExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ColonToken != default)
        {
            var colonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(colonTokenBuilder, syntaxNode.ColonToken);
            properties.Add($"\"colonToken\":{colonTokenBuilder}");
        }
        if (syntaxNode.Condition != null)
        {
            var conditionBuilder = new StringBuilder();
            WriteSyntaxNode(conditionBuilder, syntaxNode.Condition);
            properties.Add($"\"condition\":{conditionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.QuestionToken != default)
        {
            var questionTokenBuilder = new StringBuilder();
            WriteSyntaxToken(questionTokenBuilder, syntaxNode.QuestionToken);
            properties.Add($"\"questionToken\":{questionTokenBuilder}");
        }
        if (syntaxNode.WhenFalse != null)
        {
            var whenFalseBuilder = new StringBuilder();
            WriteSyntaxNode(whenFalseBuilder, syntaxNode.WhenFalse);
            properties.Add($"\"whenFalse\":{whenFalseBuilder}");
        }
        if (syntaxNode.WhenTrue != null)
        {
            var whenTrueBuilder = new StringBuilder();
            WriteSyntaxNode(whenTrueBuilder, syntaxNode.WhenTrue);
            properties.Add($"\"whenTrue\":{whenTrueBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteConstantPatternSyntax(StringBuilder builder, ConstantPatternSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteConstructorConstraintSyntax(StringBuilder builder, ConstructorConstraintSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.NewKeyword != default)
        {
            var newKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(newKeywordBuilder, syntaxNode.NewKeyword);
            properties.Add($"\"newKeyword\":{newKeywordBuilder}");
        }
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteConstructorDeclarationSyntax(StringBuilder builder, ConstructorDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Body != null)
        {
            var bodyBuilder = new StringBuilder();
            WriteBlockSyntax(bodyBuilder, syntaxNode.Body);
            properties.Add($"\"body\":{bodyBuilder}");
        }
        if (syntaxNode.ExpressionBody != null)
        {
            var expressionBodyBuilder = new StringBuilder();
            WriteArrowExpressionClauseSyntax(expressionBodyBuilder, syntaxNode.ExpressionBody);
            properties.Add($"\"expressionBody\":{expressionBodyBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        if (syntaxNode.Initializer != null)
        {
            var initializerBuilder = new StringBuilder();
            WriteConstructorInitializerSyntax(initializerBuilder, syntaxNode.Initializer);
            properties.Add($"\"initializer\":{initializerBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.ParameterList != null)
        {
            var parameterListBuilder = new StringBuilder();
            WriteParameterListSyntax(parameterListBuilder, syntaxNode.ParameterList);
            properties.Add($"\"parameterList\":{parameterListBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteConstructorInitializerSyntax(StringBuilder builder, ConstructorInitializerSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ArgumentList != null)
        {
            var argumentListBuilder = new StringBuilder();
            WriteArgumentListSyntax(argumentListBuilder, syntaxNode.ArgumentList);
            properties.Add($"\"argumentList\":{argumentListBuilder}");
        }
        if (syntaxNode.ColonToken != default)
        {
            var colonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(colonTokenBuilder, syntaxNode.ColonToken);
            properties.Add($"\"colonToken\":{colonTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.ThisOrBaseKeyword != default)
        {
            var thisOrBaseKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(thisOrBaseKeywordBuilder, syntaxNode.ThisOrBaseKeyword);
            properties.Add($"\"thisOrBaseKeyword\":{thisOrBaseKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteContinueStatementSyntax(StringBuilder builder, ContinueStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.ContinueKeyword != default)
        {
            var continueKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(continueKeywordBuilder, syntaxNode.ContinueKeyword);
            properties.Add($"\"continueKeyword\":{continueKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteConversionOperatorDeclarationSyntax(StringBuilder builder, ConversionOperatorDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Body != null)
        {
            var bodyBuilder = new StringBuilder();
            WriteBlockSyntax(bodyBuilder, syntaxNode.Body);
            properties.Add($"\"body\":{bodyBuilder}");
        }
        if (syntaxNode.CheckedKeyword != default)
        {
            var checkedKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(checkedKeywordBuilder, syntaxNode.CheckedKeyword);
            properties.Add($"\"checkedKeyword\":{checkedKeywordBuilder}");
        }
        if (syntaxNode.ExplicitInterfaceSpecifier != null)
        {
            var explicitInterfaceSpecifierBuilder = new StringBuilder();
            WriteExplicitInterfaceSpecifierSyntax(explicitInterfaceSpecifierBuilder, syntaxNode.ExplicitInterfaceSpecifier);
            properties.Add($"\"explicitInterfaceSpecifier\":{explicitInterfaceSpecifierBuilder}");
        }
        if (syntaxNode.ExpressionBody != null)
        {
            var expressionBodyBuilder = new StringBuilder();
            WriteArrowExpressionClauseSyntax(expressionBodyBuilder, syntaxNode.ExpressionBody);
            properties.Add($"\"expressionBody\":{expressionBodyBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.ImplicitOrExplicitKeyword != default)
        {
            var implicitOrExplicitKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(implicitOrExplicitKeywordBuilder, syntaxNode.ImplicitOrExplicitKeyword);
            properties.Add($"\"implicitOrExplicitKeyword\":{implicitOrExplicitKeywordBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.OperatorKeyword != default)
        {
            var operatorKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(operatorKeywordBuilder, syntaxNode.OperatorKeyword);
            properties.Add($"\"operatorKeyword\":{operatorKeywordBuilder}");
        }
        if (syntaxNode.ParameterList != null)
        {
            var parameterListBuilder = new StringBuilder();
            WriteParameterListSyntax(parameterListBuilder, syntaxNode.ParameterList);
            properties.Add($"\"parameterList\":{parameterListBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteConversionOperatorMemberCrefSyntax(StringBuilder builder, ConversionOperatorMemberCrefSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CheckedKeyword != default)
        {
            var checkedKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(checkedKeywordBuilder, syntaxNode.CheckedKeyword);
            properties.Add($"\"checkedKeyword\":{checkedKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.ImplicitOrExplicitKeyword != default)
        {
            var implicitOrExplicitKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(implicitOrExplicitKeywordBuilder, syntaxNode.ImplicitOrExplicitKeyword);
            properties.Add($"\"implicitOrExplicitKeyword\":{implicitOrExplicitKeywordBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OperatorKeyword != default)
        {
            var operatorKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(operatorKeywordBuilder, syntaxNode.OperatorKeyword);
            properties.Add($"\"operatorKeyword\":{operatorKeywordBuilder}");
        }
        if (syntaxNode.Parameters != null)
        {
            var parametersBuilder = new StringBuilder();
            WriteCrefParameterListSyntax(parametersBuilder, syntaxNode.Parameters);
            properties.Add($"\"parameters\":{parametersBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteCrefBracketedParameterListSyntax(StringBuilder builder, CrefBracketedParameterListSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseBracketToken != default)
        {
            var closeBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBracketTokenBuilder, syntaxNode.CloseBracketToken);
            properties.Add($"\"closeBracketToken\":{closeBracketTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBracketToken != default)
        {
            var openBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBracketTokenBuilder, syntaxNode.OpenBracketToken);
            properties.Add($"\"openBracketToken\":{openBracketTokenBuilder}");
        }
        var parameters = new List<string>();
        foreach (var node in syntaxNode.Parameters)
        {
            var innerBuilder = new StringBuilder();
            WriteCrefParameterSyntax(innerBuilder, node);
            parameters.Add(innerBuilder.ToString());
        }
        properties.Add($"\"parameters\":[{string.Join(",", parameters)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteCrefParameterListSyntax(StringBuilder builder, CrefParameterListSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        var parameters = new List<string>();
        foreach (var node in syntaxNode.Parameters)
        {
            var innerBuilder = new StringBuilder();
            WriteCrefParameterSyntax(innerBuilder, node);
            parameters.Add(innerBuilder.ToString());
        }
        properties.Add($"\"parameters\":[{string.Join(",", parameters)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteCrefParameterSyntax(StringBuilder builder, CrefParameterSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.ReadOnlyKeyword != default)
        {
            var readOnlyKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(readOnlyKeywordBuilder, syntaxNode.ReadOnlyKeyword);
            properties.Add($"\"readOnlyKeyword\":{readOnlyKeywordBuilder}");
        }
        if (syntaxNode.RefKindKeyword != default)
        {
            var refKindKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(refKindKeywordBuilder, syntaxNode.RefKindKeyword);
            properties.Add($"\"refKindKeyword\":{refKindKeywordBuilder}");
        }
        if (syntaxNode.RefOrOutKeyword != default)
        {
            var refOrOutKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(refOrOutKeywordBuilder, syntaxNode.RefOrOutKeyword);
            properties.Add($"\"refOrOutKeyword\":{refOrOutKeywordBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteDeclarationExpressionSyntax(StringBuilder builder, DeclarationExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Designation != null)
        {
            var designationBuilder = new StringBuilder();
            WriteSyntaxNode(designationBuilder, syntaxNode.Designation);
            properties.Add($"\"designation\":{designationBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteDeclarationPatternSyntax(StringBuilder builder, DeclarationPatternSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Designation != null)
        {
            var designationBuilder = new StringBuilder();
            WriteSyntaxNode(designationBuilder, syntaxNode.Designation);
            properties.Add($"\"designation\":{designationBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteDefaultConstraintSyntax(StringBuilder builder, DefaultConstraintSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DefaultKeyword != default)
        {
            var defaultKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(defaultKeywordBuilder, syntaxNode.DefaultKeyword);
            properties.Add($"\"defaultKeyword\":{defaultKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteDefaultExpressionSyntax(StringBuilder builder, DefaultExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteDefaultSwitchLabelSyntax(StringBuilder builder, DefaultSwitchLabelSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ColonToken != default)
        {
            var colonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(colonTokenBuilder, syntaxNode.ColonToken);
            properties.Add($"\"colonToken\":{colonTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteDefineDirectiveTriviaSyntax(StringBuilder builder, DefineDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DefineKeyword != default)
        {
            var defineKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(defineKeywordBuilder, syntaxNode.DefineKeyword);
            properties.Add($"\"defineKeyword\":{defineKeywordBuilder}");
        }
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != default)
        {
            var nameBuilder = new StringBuilder();
            WriteSyntaxToken(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteDelegateDeclarationSyntax(StringBuilder builder, DelegateDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteInt("arity", syntaxNode.Arity));
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        var constraintClauses = new List<string>();
        foreach (var node in syntaxNode.ConstraintClauses)
        {
            var innerBuilder = new StringBuilder();
            WriteTypeParameterConstraintClauseSyntax(innerBuilder, node);
            constraintClauses.Add(innerBuilder.ToString());
        }
        properties.Add($"\"constraintClauses\":[{string.Join(",", constraintClauses)}]");
        if (syntaxNode.DelegateKeyword != default)
        {
            var delegateKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(delegateKeywordBuilder, syntaxNode.DelegateKeyword);
            properties.Add($"\"delegateKeyword\":{delegateKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.ParameterList != null)
        {
            var parameterListBuilder = new StringBuilder();
            WriteParameterListSyntax(parameterListBuilder, syntaxNode.ParameterList);
            properties.Add($"\"parameterList\":{parameterListBuilder}");
        }
        if (syntaxNode.ReturnType != null)
        {
            var returnTypeBuilder = new StringBuilder();
            WriteSyntaxNode(returnTypeBuilder, syntaxNode.ReturnType);
            properties.Add($"\"returnType\":{returnTypeBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.TypeParameterList != null)
        {
            var typeParameterListBuilder = new StringBuilder();
            WriteTypeParameterListSyntax(typeParameterListBuilder, syntaxNode.TypeParameterList);
            properties.Add($"\"typeParameterList\":{typeParameterListBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteDestructorDeclarationSyntax(StringBuilder builder, DestructorDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Body != null)
        {
            var bodyBuilder = new StringBuilder();
            WriteBlockSyntax(bodyBuilder, syntaxNode.Body);
            properties.Add($"\"body\":{bodyBuilder}");
        }
        if (syntaxNode.ExpressionBody != null)
        {
            var expressionBodyBuilder = new StringBuilder();
            WriteArrowExpressionClauseSyntax(expressionBodyBuilder, syntaxNode.ExpressionBody);
            properties.Add($"\"expressionBody\":{expressionBodyBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.ParameterList != null)
        {
            var parameterListBuilder = new StringBuilder();
            WriteParameterListSyntax(parameterListBuilder, syntaxNode.ParameterList);
            properties.Add($"\"parameterList\":{parameterListBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.TildeToken != default)
        {
            var tildeTokenBuilder = new StringBuilder();
            WriteSyntaxToken(tildeTokenBuilder, syntaxNode.TildeToken);
            properties.Add($"\"tildeToken\":{tildeTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteDiscardDesignationSyntax(StringBuilder builder, DiscardDesignationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.UnderscoreToken != default)
        {
            var underscoreTokenBuilder = new StringBuilder();
            WriteSyntaxToken(underscoreTokenBuilder, syntaxNode.UnderscoreToken);
            properties.Add($"\"underscoreToken\":{underscoreTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteDiscardPatternSyntax(StringBuilder builder, DiscardPatternSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.UnderscoreToken != default)
        {
            var underscoreTokenBuilder = new StringBuilder();
            WriteSyntaxToken(underscoreTokenBuilder, syntaxNode.UnderscoreToken);
            properties.Add($"\"underscoreToken\":{underscoreTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteDocumentationCommentTriviaSyntax(StringBuilder builder, DocumentationCommentTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var content = new List<string>();
        foreach (var node in syntaxNode.Content)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            content.Add(innerBuilder.ToString());
        }
        properties.Add($"\"content\":[{string.Join(",", content)}]");
        if (syntaxNode.EndOfComment != default)
        {
            var endOfCommentBuilder = new StringBuilder();
            WriteSyntaxToken(endOfCommentBuilder, syntaxNode.EndOfComment);
            properties.Add($"\"endOfComment\":{endOfCommentBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteDoStatementSyntax(StringBuilder builder, DoStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Condition != null)
        {
            var conditionBuilder = new StringBuilder();
            WriteSyntaxNode(conditionBuilder, syntaxNode.Condition);
            properties.Add($"\"condition\":{conditionBuilder}");
        }
        if (syntaxNode.DoKeyword != default)
        {
            var doKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(doKeywordBuilder, syntaxNode.DoKeyword);
            properties.Add($"\"doKeyword\":{doKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.Statement != null)
        {
            var statementBuilder = new StringBuilder();
            WriteSyntaxNode(statementBuilder, syntaxNode.Statement);
            properties.Add($"\"statement\":{statementBuilder}");
        }
        if (syntaxNode.WhileKeyword != default)
        {
            var whileKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(whileKeywordBuilder, syntaxNode.WhileKeyword);
            properties.Add($"\"whileKeyword\":{whileKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteElementAccessExpressionSyntax(StringBuilder builder, ElementAccessExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ArgumentList != null)
        {
            var argumentListBuilder = new StringBuilder();
            WriteBracketedArgumentListSyntax(argumentListBuilder, syntaxNode.ArgumentList);
            properties.Add($"\"argumentList\":{argumentListBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteElementBindingExpressionSyntax(StringBuilder builder, ElementBindingExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ArgumentList != null)
        {
            var argumentListBuilder = new StringBuilder();
            WriteBracketedArgumentListSyntax(argumentListBuilder, syntaxNode.ArgumentList);
            properties.Add($"\"argumentList\":{argumentListBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteElifDirectiveTriviaSyntax(StringBuilder builder, ElifDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("branchTaken", syntaxNode.BranchTaken));
        if (syntaxNode.Condition != null)
        {
            var conditionBuilder = new StringBuilder();
            WriteSyntaxNode(conditionBuilder, syntaxNode.Condition);
            properties.Add($"\"condition\":{conditionBuilder}");
        }
        properties.Add(WriteBoolean("conditionValue", syntaxNode.ConditionValue));
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.ElifKeyword != default)
        {
            var elifKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(elifKeywordBuilder, syntaxNode.ElifKeyword);
            properties.Add($"\"elifKeyword\":{elifKeywordBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteElseClauseSyntax(StringBuilder builder, ElseClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ElseKeyword != default)
        {
            var elseKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(elseKeywordBuilder, syntaxNode.ElseKeyword);
            properties.Add($"\"elseKeyword\":{elseKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Statement != null)
        {
            var statementBuilder = new StringBuilder();
            WriteSyntaxNode(statementBuilder, syntaxNode.Statement);
            properties.Add($"\"statement\":{statementBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteElseDirectiveTriviaSyntax(StringBuilder builder, ElseDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("branchTaken", syntaxNode.BranchTaken));
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.ElseKeyword != default)
        {
            var elseKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(elseKeywordBuilder, syntaxNode.ElseKeyword);
            properties.Add($"\"elseKeyword\":{elseKeywordBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteEmptyStatementSyntax(StringBuilder builder, EmptyStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteEndIfDirectiveTriviaSyntax(StringBuilder builder, EndIfDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndIfKeyword != default)
        {
            var endIfKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(endIfKeywordBuilder, syntaxNode.EndIfKeyword);
            properties.Add($"\"endIfKeyword\":{endIfKeywordBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteEndRegionDirectiveTriviaSyntax(StringBuilder builder, EndRegionDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.EndRegionKeyword != default)
        {
            var endRegionKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(endRegionKeywordBuilder, syntaxNode.EndRegionKeyword);
            properties.Add($"\"endRegionKeyword\":{endRegionKeywordBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteEnumDeclarationSyntax(StringBuilder builder, EnumDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.BaseList != null)
        {
            var baseListBuilder = new StringBuilder();
            WriteBaseListSyntax(baseListBuilder, syntaxNode.BaseList);
            properties.Add($"\"baseList\":{baseListBuilder}");
        }
        if (syntaxNode.CloseBraceToken != default)
        {
            var closeBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBraceTokenBuilder, syntaxNode.CloseBraceToken);
            properties.Add($"\"closeBraceToken\":{closeBraceTokenBuilder}");
        }
        if (syntaxNode.EnumKeyword != default)
        {
            var enumKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(enumKeywordBuilder, syntaxNode.EnumKeyword);
            properties.Add($"\"enumKeyword\":{enumKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var members = new List<string>();
        foreach (var node in syntaxNode.Members)
        {
            var innerBuilder = new StringBuilder();
            WriteEnumMemberDeclarationSyntax(innerBuilder, node);
            members.Add(innerBuilder.ToString());
        }
        properties.Add($"\"members\":[{string.Join(",", members)}]");
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.OpenBraceToken != default)
        {
            var openBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBraceTokenBuilder, syntaxNode.OpenBraceToken);
            properties.Add($"\"openBraceToken\":{openBraceTokenBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteEnumMemberDeclarationSyntax(StringBuilder builder, EnumMemberDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.EqualsValue != null)
        {
            var equalsValueBuilder = new StringBuilder();
            WriteEqualsValueClauseSyntax(equalsValueBuilder, syntaxNode.EqualsValue);
            properties.Add($"\"equalsValue\":{equalsValueBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteEqualsValueClauseSyntax(StringBuilder builder, EqualsValueClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.EqualsToken != default)
        {
            var equalsTokenBuilder = new StringBuilder();
            WriteSyntaxToken(equalsTokenBuilder, syntaxNode.EqualsToken);
            properties.Add($"\"equalsToken\":{equalsTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Value != null)
        {
            var valueBuilder = new StringBuilder();
            WriteSyntaxNode(valueBuilder, syntaxNode.Value);
            properties.Add($"\"value\":{valueBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteErrorDirectiveTriviaSyntax(StringBuilder builder, ErrorDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.ErrorKeyword != default)
        {
            var errorKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(errorKeywordBuilder, syntaxNode.ErrorKeyword);
            properties.Add($"\"errorKeyword\":{errorKeywordBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteEventDeclarationSyntax(StringBuilder builder, EventDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.AccessorList != null)
        {
            var accessorListBuilder = new StringBuilder();
            WriteAccessorListSyntax(accessorListBuilder, syntaxNode.AccessorList);
            properties.Add($"\"accessorList\":{accessorListBuilder}");
        }
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.EventKeyword != default)
        {
            var eventKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(eventKeywordBuilder, syntaxNode.EventKeyword);
            properties.Add($"\"eventKeyword\":{eventKeywordBuilder}");
        }
        if (syntaxNode.ExplicitInterfaceSpecifier != null)
        {
            var explicitInterfaceSpecifierBuilder = new StringBuilder();
            WriteExplicitInterfaceSpecifierSyntax(explicitInterfaceSpecifierBuilder, syntaxNode.ExplicitInterfaceSpecifier);
            properties.Add($"\"explicitInterfaceSpecifier\":{explicitInterfaceSpecifierBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteEventFieldDeclarationSyntax(StringBuilder builder, EventFieldDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Declaration != null)
        {
            var declarationBuilder = new StringBuilder();
            WriteVariableDeclarationSyntax(declarationBuilder, syntaxNode.Declaration);
            properties.Add($"\"declaration\":{declarationBuilder}");
        }
        if (syntaxNode.EventKeyword != default)
        {
            var eventKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(eventKeywordBuilder, syntaxNode.EventKeyword);
            properties.Add($"\"eventKeyword\":{eventKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteExplicitInterfaceSpecifierSyntax(StringBuilder builder, ExplicitInterfaceSpecifierSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DotToken != default)
        {
            var dotTokenBuilder = new StringBuilder();
            WriteSyntaxToken(dotTokenBuilder, syntaxNode.DotToken);
            properties.Add($"\"dotToken\":{dotTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteSyntaxNode(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteExpressionColonSyntax(StringBuilder builder, ExpressionColonSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ColonToken != default)
        {
            var colonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(colonTokenBuilder, syntaxNode.ColonToken);
            properties.Add($"\"colonToken\":{colonTokenBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteExpressionElementSyntax(StringBuilder builder, ExpressionElementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteExpressionStatementSyntax(StringBuilder builder, ExpressionStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("allowsAnyExpression", syntaxNode.AllowsAnyExpression));
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteExternAliasDirectiveSyntax(StringBuilder builder, ExternAliasDirectiveSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.AliasKeyword != default)
        {
            var aliasKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(aliasKeywordBuilder, syntaxNode.AliasKeyword);
            properties.Add($"\"aliasKeyword\":{aliasKeywordBuilder}");
        }
        if (syntaxNode.ExternKeyword != default)
        {
            var externKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(externKeywordBuilder, syntaxNode.ExternKeyword);
            properties.Add($"\"externKeyword\":{externKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteFieldDeclarationSyntax(StringBuilder builder, FieldDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Declaration != null)
        {
            var declarationBuilder = new StringBuilder();
            WriteVariableDeclarationSyntax(declarationBuilder, syntaxNode.Declaration);
            properties.Add($"\"declaration\":{declarationBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteFieldExpressionSyntax(StringBuilder builder, FieldExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Token != default)
        {
            var tokenBuilder = new StringBuilder();
            WriteSyntaxToken(tokenBuilder, syntaxNode.Token);
            properties.Add($"\"token\":{tokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteFileScopedNamespaceDeclarationSyntax(StringBuilder builder, FileScopedNamespaceDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        var externs = new List<string>();
        foreach (var node in syntaxNode.Externs)
        {
            var innerBuilder = new StringBuilder();
            WriteExternAliasDirectiveSyntax(innerBuilder, node);
            externs.Add(innerBuilder.ToString());
        }
        properties.Add($"\"externs\":[{string.Join(",", externs)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var members = new List<string>();
        foreach (var node in syntaxNode.Members)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            members.Add(innerBuilder.ToString());
        }
        properties.Add($"\"members\":[{string.Join(",", members)}]");
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteSyntaxNode(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        if (syntaxNode.NamespaceKeyword != default)
        {
            var namespaceKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(namespaceKeywordBuilder, syntaxNode.NamespaceKeyword);
            properties.Add($"\"namespaceKeyword\":{namespaceKeywordBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        var usings = new List<string>();
        foreach (var node in syntaxNode.Usings)
        {
            var innerBuilder = new StringBuilder();
            WriteUsingDirectiveSyntax(innerBuilder, node);
            usings.Add(innerBuilder.ToString());
        }
        properties.Add($"\"usings\":[{string.Join(",", usings)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteFinallyClauseSyntax(StringBuilder builder, FinallyClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Block != null)
        {
            var blockBuilder = new StringBuilder();
            WriteBlockSyntax(blockBuilder, syntaxNode.Block);
            properties.Add($"\"block\":{blockBuilder}");
        }
        if (syntaxNode.FinallyKeyword != default)
        {
            var finallyKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(finallyKeywordBuilder, syntaxNode.FinallyKeyword);
            properties.Add($"\"finallyKeyword\":{finallyKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteFixedStatementSyntax(StringBuilder builder, FixedStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Declaration != null)
        {
            var declarationBuilder = new StringBuilder();
            WriteVariableDeclarationSyntax(declarationBuilder, syntaxNode.Declaration);
            properties.Add($"\"declaration\":{declarationBuilder}");
        }
        if (syntaxNode.FixedKeyword != default)
        {
            var fixedKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(fixedKeywordBuilder, syntaxNode.FixedKeyword);
            properties.Add($"\"fixedKeyword\":{fixedKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.Statement != null)
        {
            var statementBuilder = new StringBuilder();
            WriteSyntaxNode(statementBuilder, syntaxNode.Statement);
            properties.Add($"\"statement\":{statementBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteForEachStatementSyntax(StringBuilder builder, ForEachStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.AwaitKeyword != default)
        {
            var awaitKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(awaitKeywordBuilder, syntaxNode.AwaitKeyword);
            properties.Add($"\"awaitKeyword\":{awaitKeywordBuilder}");
        }
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        if (syntaxNode.ForEachKeyword != default)
        {
            var forEachKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(forEachKeywordBuilder, syntaxNode.ForEachKeyword);
            properties.Add($"\"forEachKeyword\":{forEachKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        if (syntaxNode.InKeyword != default)
        {
            var inKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(inKeywordBuilder, syntaxNode.InKeyword);
            properties.Add($"\"inKeyword\":{inKeywordBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.Statement != null)
        {
            var statementBuilder = new StringBuilder();
            WriteSyntaxNode(statementBuilder, syntaxNode.Statement);
            properties.Add($"\"statement\":{statementBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteForEachVariableStatementSyntax(StringBuilder builder, ForEachVariableStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.AwaitKeyword != default)
        {
            var awaitKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(awaitKeywordBuilder, syntaxNode.AwaitKeyword);
            properties.Add($"\"awaitKeyword\":{awaitKeywordBuilder}");
        }
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        if (syntaxNode.ForEachKeyword != default)
        {
            var forEachKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(forEachKeywordBuilder, syntaxNode.ForEachKeyword);
            properties.Add($"\"forEachKeyword\":{forEachKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.InKeyword != default)
        {
            var inKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(inKeywordBuilder, syntaxNode.InKeyword);
            properties.Add($"\"inKeyword\":{inKeywordBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.Statement != null)
        {
            var statementBuilder = new StringBuilder();
            WriteSyntaxNode(statementBuilder, syntaxNode.Statement);
            properties.Add($"\"statement\":{statementBuilder}");
        }
        if (syntaxNode.Variable != null)
        {
            var variableBuilder = new StringBuilder();
            WriteSyntaxNode(variableBuilder, syntaxNode.Variable);
            properties.Add($"\"variable\":{variableBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteForStatementSyntax(StringBuilder builder, ForStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Condition != null)
        {
            var conditionBuilder = new StringBuilder();
            WriteSyntaxNode(conditionBuilder, syntaxNode.Condition);
            properties.Add($"\"condition\":{conditionBuilder}");
        }
        if (syntaxNode.Declaration != null)
        {
            var declarationBuilder = new StringBuilder();
            WriteVariableDeclarationSyntax(declarationBuilder, syntaxNode.Declaration);
            properties.Add($"\"declaration\":{declarationBuilder}");
        }
        if (syntaxNode.FirstSemicolonToken != default)
        {
            var firstSemicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(firstSemicolonTokenBuilder, syntaxNode.FirstSemicolonToken);
            properties.Add($"\"firstSemicolonToken\":{firstSemicolonTokenBuilder}");
        }
        if (syntaxNode.ForKeyword != default)
        {
            var forKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(forKeywordBuilder, syntaxNode.ForKeyword);
            properties.Add($"\"forKeyword\":{forKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        var incrementors = new List<string>();
        foreach (var node in syntaxNode.Incrementors)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            incrementors.Add(innerBuilder.ToString());
        }
        properties.Add($"\"incrementors\":[{string.Join(",", incrementors)}]");
        var initializers = new List<string>();
        foreach (var node in syntaxNode.Initializers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            initializers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"initializers\":[{string.Join(",", initializers)}]");
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.SecondSemicolonToken != default)
        {
            var secondSemicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(secondSemicolonTokenBuilder, syntaxNode.SecondSemicolonToken);
            properties.Add($"\"secondSemicolonToken\":{secondSemicolonTokenBuilder}");
        }
        if (syntaxNode.Statement != null)
        {
            var statementBuilder = new StringBuilder();
            WriteSyntaxNode(statementBuilder, syntaxNode.Statement);
            properties.Add($"\"statement\":{statementBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteFromClauseSyntax(StringBuilder builder, FromClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        if (syntaxNode.FromKeyword != default)
        {
            var fromKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(fromKeywordBuilder, syntaxNode.FromKeyword);
            properties.Add($"\"fromKeyword\":{fromKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        if (syntaxNode.InKeyword != default)
        {
            var inKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(inKeywordBuilder, syntaxNode.InKeyword);
            properties.Add($"\"inKeyword\":{inKeywordBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteFunctionPointerCallingConventionSyntax(StringBuilder builder, FunctionPointerCallingConventionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.ManagedOrUnmanagedKeyword != default)
        {
            var managedOrUnmanagedKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(managedOrUnmanagedKeywordBuilder, syntaxNode.ManagedOrUnmanagedKeyword);
            properties.Add($"\"managedOrUnmanagedKeyword\":{managedOrUnmanagedKeywordBuilder}");
        }
        if (syntaxNode.UnmanagedCallingConventionList != null)
        {
            var unmanagedCallingConventionListBuilder = new StringBuilder();
            WriteFunctionPointerUnmanagedCallingConventionListSyntax(unmanagedCallingConventionListBuilder, syntaxNode.UnmanagedCallingConventionList);
            properties.Add($"\"unmanagedCallingConventionList\":{unmanagedCallingConventionListBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteFunctionPointerParameterListSyntax(StringBuilder builder, FunctionPointerParameterListSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.GreaterThanToken != default)
        {
            var greaterThanTokenBuilder = new StringBuilder();
            WriteSyntaxToken(greaterThanTokenBuilder, syntaxNode.GreaterThanToken);
            properties.Add($"\"greaterThanToken\":{greaterThanTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.LessThanToken != default)
        {
            var lessThanTokenBuilder = new StringBuilder();
            WriteSyntaxToken(lessThanTokenBuilder, syntaxNode.LessThanToken);
            properties.Add($"\"lessThanToken\":{lessThanTokenBuilder}");
        }
        var parameters = new List<string>();
        foreach (var node in syntaxNode.Parameters)
        {
            var innerBuilder = new StringBuilder();
            WriteFunctionPointerParameterSyntax(innerBuilder, node);
            parameters.Add(innerBuilder.ToString());
        }
        properties.Add($"\"parameters\":[{string.Join(",", parameters)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteFunctionPointerParameterSyntax(StringBuilder builder, FunctionPointerParameterSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteFunctionPointerTypeSyntax(StringBuilder builder, FunctionPointerTypeSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.AsteriskToken != default)
        {
            var asteriskTokenBuilder = new StringBuilder();
            WriteSyntaxToken(asteriskTokenBuilder, syntaxNode.AsteriskToken);
            properties.Add($"\"asteriskToken\":{asteriskTokenBuilder}");
        }
        if (syntaxNode.CallingConvention != null)
        {
            var callingConventionBuilder = new StringBuilder();
            WriteFunctionPointerCallingConventionSyntax(callingConventionBuilder, syntaxNode.CallingConvention);
            properties.Add($"\"callingConvention\":{callingConventionBuilder}");
        }
        if (syntaxNode.DelegateKeyword != default)
        {
            var delegateKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(delegateKeywordBuilder, syntaxNode.DelegateKeyword);
            properties.Add($"\"delegateKeyword\":{delegateKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        properties.Add(WriteBoolean("isNint", syntaxNode.IsNint));
        properties.Add(WriteBoolean("isNotNull", syntaxNode.IsNotNull));
        properties.Add(WriteBoolean("isNuint", syntaxNode.IsNuint));
        properties.Add(WriteBoolean("isUnmanaged", syntaxNode.IsUnmanaged));
        properties.Add(WriteBoolean("isVar", syntaxNode.IsVar));
        if (syntaxNode.ParameterList != null)
        {
            var parameterListBuilder = new StringBuilder();
            WriteFunctionPointerParameterListSyntax(parameterListBuilder, syntaxNode.ParameterList);
            properties.Add($"\"parameterList\":{parameterListBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteFunctionPointerUnmanagedCallingConventionListSyntax(StringBuilder builder, FunctionPointerUnmanagedCallingConventionListSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var callingConventions = new List<string>();
        foreach (var node in syntaxNode.CallingConventions)
        {
            var innerBuilder = new StringBuilder();
            WriteFunctionPointerUnmanagedCallingConventionSyntax(innerBuilder, node);
            callingConventions.Add(innerBuilder.ToString());
        }
        properties.Add($"\"callingConventions\":[{string.Join(",", callingConventions)}]");
        if (syntaxNode.CloseBracketToken != default)
        {
            var closeBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBracketTokenBuilder, syntaxNode.CloseBracketToken);
            properties.Add($"\"closeBracketToken\":{closeBracketTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBracketToken != default)
        {
            var openBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBracketTokenBuilder, syntaxNode.OpenBracketToken);
            properties.Add($"\"openBracketToken\":{openBracketTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteFunctionPointerUnmanagedCallingConventionSyntax(StringBuilder builder, FunctionPointerUnmanagedCallingConventionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != default)
        {
            var nameBuilder = new StringBuilder();
            WriteSyntaxToken(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteGenericNameSyntax(StringBuilder builder, GenericNameSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteInt("arity", syntaxNode.Arity));
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        properties.Add(WriteBoolean("isNint", syntaxNode.IsNint));
        properties.Add(WriteBoolean("isNotNull", syntaxNode.IsNotNull));
        properties.Add(WriteBoolean("isNuint", syntaxNode.IsNuint));
        properties.Add(WriteBoolean("isUnboundGenericName", syntaxNode.IsUnboundGenericName));
        properties.Add(WriteBoolean("isUnmanaged", syntaxNode.IsUnmanaged));
        properties.Add(WriteBoolean("isVar", syntaxNode.IsVar));
        if (syntaxNode.TypeArgumentList != null)
        {
            var typeArgumentListBuilder = new StringBuilder();
            WriteTypeArgumentListSyntax(typeArgumentListBuilder, syntaxNode.TypeArgumentList);
            properties.Add($"\"typeArgumentList\":{typeArgumentListBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteGlobalStatementSyntax(StringBuilder builder, GlobalStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.Statement != null)
        {
            var statementBuilder = new StringBuilder();
            WriteSyntaxNode(statementBuilder, syntaxNode.Statement);
            properties.Add($"\"statement\":{statementBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteGotoStatementSyntax(StringBuilder builder, GotoStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.CaseOrDefaultKeyword != default)
        {
            var caseOrDefaultKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(caseOrDefaultKeywordBuilder, syntaxNode.CaseOrDefaultKeyword);
            properties.Add($"\"caseOrDefaultKeyword\":{caseOrDefaultKeywordBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        if (syntaxNode.GotoKeyword != default)
        {
            var gotoKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(gotoKeywordBuilder, syntaxNode.GotoKeyword);
            properties.Add($"\"gotoKeyword\":{gotoKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteGroupClauseSyntax(StringBuilder builder, GroupClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ByExpression != null)
        {
            var byExpressionBuilder = new StringBuilder();
            WriteSyntaxNode(byExpressionBuilder, syntaxNode.ByExpression);
            properties.Add($"\"byExpression\":{byExpressionBuilder}");
        }
        if (syntaxNode.ByKeyword != default)
        {
            var byKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(byKeywordBuilder, syntaxNode.ByKeyword);
            properties.Add($"\"byKeyword\":{byKeywordBuilder}");
        }
        if (syntaxNode.GroupExpression != null)
        {
            var groupExpressionBuilder = new StringBuilder();
            WriteSyntaxNode(groupExpressionBuilder, syntaxNode.GroupExpression);
            properties.Add($"\"groupExpression\":{groupExpressionBuilder}");
        }
        if (syntaxNode.GroupKeyword != default)
        {
            var groupKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(groupKeywordBuilder, syntaxNode.GroupKeyword);
            properties.Add($"\"groupKeyword\":{groupKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteIdentifierNameSyntax(StringBuilder builder, IdentifierNameSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteInt("arity", syntaxNode.Arity));
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        properties.Add(WriteBoolean("isNint", syntaxNode.IsNint));
        properties.Add(WriteBoolean("isNotNull", syntaxNode.IsNotNull));
        properties.Add(WriteBoolean("isNuint", syntaxNode.IsNuint));
        properties.Add(WriteBoolean("isUnmanaged", syntaxNode.IsUnmanaged));
        properties.Add(WriteBoolean("isVar", syntaxNode.IsVar));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteIfDirectiveTriviaSyntax(StringBuilder builder, IfDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("branchTaken", syntaxNode.BranchTaken));
        if (syntaxNode.Condition != null)
        {
            var conditionBuilder = new StringBuilder();
            WriteSyntaxNode(conditionBuilder, syntaxNode.Condition);
            properties.Add($"\"condition\":{conditionBuilder}");
        }
        properties.Add(WriteBoolean("conditionValue", syntaxNode.ConditionValue));
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.IfKeyword != default)
        {
            var ifKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(ifKeywordBuilder, syntaxNode.IfKeyword);
            properties.Add($"\"ifKeyword\":{ifKeywordBuilder}");
        }
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteIfStatementSyntax(StringBuilder builder, IfStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Condition != null)
        {
            var conditionBuilder = new StringBuilder();
            WriteSyntaxNode(conditionBuilder, syntaxNode.Condition);
            properties.Add($"\"condition\":{conditionBuilder}");
        }
        if (syntaxNode.Else != null)
        {
            var elseBuilder = new StringBuilder();
            WriteElseClauseSyntax(elseBuilder, syntaxNode.Else);
            properties.Add($"\"else\":{elseBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.IfKeyword != default)
        {
            var ifKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(ifKeywordBuilder, syntaxNode.IfKeyword);
            properties.Add($"\"ifKeyword\":{ifKeywordBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.Statement != null)
        {
            var statementBuilder = new StringBuilder();
            WriteSyntaxNode(statementBuilder, syntaxNode.Statement);
            properties.Add($"\"statement\":{statementBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteIgnoredDirectiveTriviaSyntax(StringBuilder builder, IgnoredDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ColonToken != default)
        {
            var colonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(colonTokenBuilder, syntaxNode.ColonToken);
            properties.Add($"\"colonToken\":{colonTokenBuilder}");
        }
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteImplicitArrayCreationExpressionSyntax(StringBuilder builder, ImplicitArrayCreationExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseBracketToken != default)
        {
            var closeBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBracketTokenBuilder, syntaxNode.CloseBracketToken);
            properties.Add($"\"closeBracketToken\":{closeBracketTokenBuilder}");
        }
        var commas = new List<string>();
        foreach (var node in syntaxNode.Commas)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            commas.Add(innerBuilder.ToString());
        }
        properties.Add($"\"commas\":[{string.Join(",", commas)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Initializer != null)
        {
            var initializerBuilder = new StringBuilder();
            WriteInitializerExpressionSyntax(initializerBuilder, syntaxNode.Initializer);
            properties.Add($"\"initializer\":{initializerBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.NewKeyword != default)
        {
            var newKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(newKeywordBuilder, syntaxNode.NewKeyword);
            properties.Add($"\"newKeyword\":{newKeywordBuilder}");
        }
        if (syntaxNode.OpenBracketToken != default)
        {
            var openBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBracketTokenBuilder, syntaxNode.OpenBracketToken);
            properties.Add($"\"openBracketToken\":{openBracketTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteImplicitElementAccessSyntax(StringBuilder builder, ImplicitElementAccessSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ArgumentList != null)
        {
            var argumentListBuilder = new StringBuilder();
            WriteBracketedArgumentListSyntax(argumentListBuilder, syntaxNode.ArgumentList);
            properties.Add($"\"argumentList\":{argumentListBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteImplicitObjectCreationExpressionSyntax(StringBuilder builder, ImplicitObjectCreationExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ArgumentList != null)
        {
            var argumentListBuilder = new StringBuilder();
            WriteArgumentListSyntax(argumentListBuilder, syntaxNode.ArgumentList);
            properties.Add($"\"argumentList\":{argumentListBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Initializer != null)
        {
            var initializerBuilder = new StringBuilder();
            WriteInitializerExpressionSyntax(initializerBuilder, syntaxNode.Initializer);
            properties.Add($"\"initializer\":{initializerBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.NewKeyword != default)
        {
            var newKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(newKeywordBuilder, syntaxNode.NewKeyword);
            properties.Add($"\"newKeyword\":{newKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteImplicitStackAllocArrayCreationExpressionSyntax(StringBuilder builder, ImplicitStackAllocArrayCreationExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseBracketToken != default)
        {
            var closeBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBracketTokenBuilder, syntaxNode.CloseBracketToken);
            properties.Add($"\"closeBracketToken\":{closeBracketTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Initializer != null)
        {
            var initializerBuilder = new StringBuilder();
            WriteInitializerExpressionSyntax(initializerBuilder, syntaxNode.Initializer);
            properties.Add($"\"initializer\":{initializerBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBracketToken != default)
        {
            var openBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBracketTokenBuilder, syntaxNode.OpenBracketToken);
            properties.Add($"\"openBracketToken\":{openBracketTokenBuilder}");
        }
        if (syntaxNode.StackAllocKeyword != default)
        {
            var stackAllocKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(stackAllocKeywordBuilder, syntaxNode.StackAllocKeyword);
            properties.Add($"\"stackAllocKeyword\":{stackAllocKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteIncompleteMemberSyntax(StringBuilder builder, IncompleteMemberSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteIndexerDeclarationSyntax(StringBuilder builder, IndexerDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.AccessorList != null)
        {
            var accessorListBuilder = new StringBuilder();
            WriteAccessorListSyntax(accessorListBuilder, syntaxNode.AccessorList);
            properties.Add($"\"accessorList\":{accessorListBuilder}");
        }
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.ExplicitInterfaceSpecifier != null)
        {
            var explicitInterfaceSpecifierBuilder = new StringBuilder();
            WriteExplicitInterfaceSpecifierSyntax(explicitInterfaceSpecifierBuilder, syntaxNode.ExplicitInterfaceSpecifier);
            properties.Add($"\"explicitInterfaceSpecifier\":{explicitInterfaceSpecifierBuilder}");
        }
        if (syntaxNode.ExpressionBody != null)
        {
            var expressionBodyBuilder = new StringBuilder();
            WriteArrowExpressionClauseSyntax(expressionBodyBuilder, syntaxNode.ExpressionBody);
            properties.Add($"\"expressionBody\":{expressionBodyBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.ParameterList != null)
        {
            var parameterListBuilder = new StringBuilder();
            WriteBracketedParameterListSyntax(parameterListBuilder, syntaxNode.ParameterList);
            properties.Add($"\"parameterList\":{parameterListBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.ThisKeyword != default)
        {
            var thisKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(thisKeywordBuilder, syntaxNode.ThisKeyword);
            properties.Add($"\"thisKeyword\":{thisKeywordBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteIndexerMemberCrefSyntax(StringBuilder builder, IndexerMemberCrefSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Parameters != null)
        {
            var parametersBuilder = new StringBuilder();
            WriteCrefBracketedParameterListSyntax(parametersBuilder, syntaxNode.Parameters);
            properties.Add($"\"parameters\":{parametersBuilder}");
        }
        if (syntaxNode.ThisKeyword != default)
        {
            var thisKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(thisKeywordBuilder, syntaxNode.ThisKeyword);
            properties.Add($"\"thisKeyword\":{thisKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteInitializerExpressionSyntax(StringBuilder builder, InitializerExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseBraceToken != default)
        {
            var closeBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBraceTokenBuilder, syntaxNode.CloseBraceToken);
            properties.Add($"\"closeBraceToken\":{closeBraceTokenBuilder}");
        }
        var expressions = new List<string>();
        foreach (var node in syntaxNode.Expressions)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            expressions.Add(innerBuilder.ToString());
        }
        properties.Add($"\"expressions\":[{string.Join(",", expressions)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBraceToken != default)
        {
            var openBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBraceTokenBuilder, syntaxNode.OpenBraceToken);
            properties.Add($"\"openBraceToken\":{openBraceTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteInterfaceDeclarationSyntax(StringBuilder builder, InterfaceDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteInt("arity", syntaxNode.Arity));
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.BaseList != null)
        {
            var baseListBuilder = new StringBuilder();
            WriteBaseListSyntax(baseListBuilder, syntaxNode.BaseList);
            properties.Add($"\"baseList\":{baseListBuilder}");
        }
        if (syntaxNode.CloseBraceToken != default)
        {
            var closeBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBraceTokenBuilder, syntaxNode.CloseBraceToken);
            properties.Add($"\"closeBraceToken\":{closeBraceTokenBuilder}");
        }
        var constraintClauses = new List<string>();
        foreach (var node in syntaxNode.ConstraintClauses)
        {
            var innerBuilder = new StringBuilder();
            WriteTypeParameterConstraintClauseSyntax(innerBuilder, node);
            constraintClauses.Add(innerBuilder.ToString());
        }
        properties.Add($"\"constraintClauses\":[{string.Join(",", constraintClauses)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        var members = new List<string>();
        foreach (var node in syntaxNode.Members)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            members.Add(innerBuilder.ToString());
        }
        properties.Add($"\"members\":[{string.Join(",", members)}]");
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.OpenBraceToken != default)
        {
            var openBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBraceTokenBuilder, syntaxNode.OpenBraceToken);
            properties.Add($"\"openBraceToken\":{openBraceTokenBuilder}");
        }
        if (syntaxNode.ParameterList != null)
        {
            var parameterListBuilder = new StringBuilder();
            WriteParameterListSyntax(parameterListBuilder, syntaxNode.ParameterList);
            properties.Add($"\"parameterList\":{parameterListBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.TypeParameterList != null)
        {
            var typeParameterListBuilder = new StringBuilder();
            WriteTypeParameterListSyntax(typeParameterListBuilder, syntaxNode.TypeParameterList);
            properties.Add($"\"typeParameterList\":{typeParameterListBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteInterpolatedStringExpressionSyntax(StringBuilder builder, InterpolatedStringExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var contents = new List<string>();
        foreach (var node in syntaxNode.Contents)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            contents.Add(innerBuilder.ToString());
        }
        properties.Add($"\"contents\":[{string.Join(",", contents)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.StringEndToken != default)
        {
            var stringEndTokenBuilder = new StringBuilder();
            WriteSyntaxToken(stringEndTokenBuilder, syntaxNode.StringEndToken);
            properties.Add($"\"stringEndToken\":{stringEndTokenBuilder}");
        }
        if (syntaxNode.StringStartToken != default)
        {
            var stringStartTokenBuilder = new StringBuilder();
            WriteSyntaxToken(stringStartTokenBuilder, syntaxNode.StringStartToken);
            properties.Add($"\"stringStartToken\":{stringStartTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteInterpolatedStringTextSyntax(StringBuilder builder, InterpolatedStringTextSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.TextToken != default)
        {
            var textTokenBuilder = new StringBuilder();
            WriteSyntaxToken(textTokenBuilder, syntaxNode.TextToken);
            properties.Add($"\"textToken\":{textTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteInterpolationAlignmentClauseSyntax(StringBuilder builder, InterpolationAlignmentClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CommaToken != default)
        {
            var commaTokenBuilder = new StringBuilder();
            WriteSyntaxToken(commaTokenBuilder, syntaxNode.CommaToken);
            properties.Add($"\"commaToken\":{commaTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Value != null)
        {
            var valueBuilder = new StringBuilder();
            WriteSyntaxNode(valueBuilder, syntaxNode.Value);
            properties.Add($"\"value\":{valueBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteInterpolationFormatClauseSyntax(StringBuilder builder, InterpolationFormatClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ColonToken != default)
        {
            var colonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(colonTokenBuilder, syntaxNode.ColonToken);
            properties.Add($"\"colonToken\":{colonTokenBuilder}");
        }
        if (syntaxNode.FormatStringToken != default)
        {
            var formatStringTokenBuilder = new StringBuilder();
            WriteSyntaxToken(formatStringTokenBuilder, syntaxNode.FormatStringToken);
            properties.Add($"\"formatStringToken\":{formatStringTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteInterpolationSyntax(StringBuilder builder, InterpolationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.AlignmentClause != null)
        {
            var alignmentClauseBuilder = new StringBuilder();
            WriteInterpolationAlignmentClauseSyntax(alignmentClauseBuilder, syntaxNode.AlignmentClause);
            properties.Add($"\"alignmentClause\":{alignmentClauseBuilder}");
        }
        if (syntaxNode.CloseBraceToken != default)
        {
            var closeBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBraceTokenBuilder, syntaxNode.CloseBraceToken);
            properties.Add($"\"closeBraceToken\":{closeBraceTokenBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        if (syntaxNode.FormatClause != null)
        {
            var formatClauseBuilder = new StringBuilder();
            WriteInterpolationFormatClauseSyntax(formatClauseBuilder, syntaxNode.FormatClause);
            properties.Add($"\"formatClause\":{formatClauseBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBraceToken != default)
        {
            var openBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBraceTokenBuilder, syntaxNode.OpenBraceToken);
            properties.Add($"\"openBraceToken\":{openBraceTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteInvocationExpressionSyntax(StringBuilder builder, InvocationExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ArgumentList != null)
        {
            var argumentListBuilder = new StringBuilder();
            WriteArgumentListSyntax(argumentListBuilder, syntaxNode.ArgumentList);
            properties.Add($"\"argumentList\":{argumentListBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteIsPatternExpressionSyntax(StringBuilder builder, IsPatternExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.IsKeyword != default)
        {
            var isKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(isKeywordBuilder, syntaxNode.IsKeyword);
            properties.Add($"\"isKeyword\":{isKeywordBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Pattern != null)
        {
            var patternBuilder = new StringBuilder();
            WriteSyntaxNode(patternBuilder, syntaxNode.Pattern);
            properties.Add($"\"pattern\":{patternBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteJoinClauseSyntax(StringBuilder builder, JoinClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.EqualsKeyword != default)
        {
            var equalsKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(equalsKeywordBuilder, syntaxNode.EqualsKeyword);
            properties.Add($"\"equalsKeyword\":{equalsKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        if (syntaxNode.InExpression != null)
        {
            var inExpressionBuilder = new StringBuilder();
            WriteSyntaxNode(inExpressionBuilder, syntaxNode.InExpression);
            properties.Add($"\"inExpression\":{inExpressionBuilder}");
        }
        if (syntaxNode.InKeyword != default)
        {
            var inKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(inKeywordBuilder, syntaxNode.InKeyword);
            properties.Add($"\"inKeyword\":{inKeywordBuilder}");
        }
        if (syntaxNode.Into != null)
        {
            var intoBuilder = new StringBuilder();
            WriteJoinIntoClauseSyntax(intoBuilder, syntaxNode.Into);
            properties.Add($"\"into\":{intoBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.JoinKeyword != default)
        {
            var joinKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(joinKeywordBuilder, syntaxNode.JoinKeyword);
            properties.Add($"\"joinKeyword\":{joinKeywordBuilder}");
        }
        if (syntaxNode.LeftExpression != null)
        {
            var leftExpressionBuilder = new StringBuilder();
            WriteSyntaxNode(leftExpressionBuilder, syntaxNode.LeftExpression);
            properties.Add($"\"leftExpression\":{leftExpressionBuilder}");
        }
        if (syntaxNode.OnKeyword != default)
        {
            var onKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(onKeywordBuilder, syntaxNode.OnKeyword);
            properties.Add($"\"onKeyword\":{onKeywordBuilder}");
        }
        if (syntaxNode.RightExpression != null)
        {
            var rightExpressionBuilder = new StringBuilder();
            WriteSyntaxNode(rightExpressionBuilder, syntaxNode.RightExpression);
            properties.Add($"\"rightExpression\":{rightExpressionBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteJoinIntoClauseSyntax(StringBuilder builder, JoinIntoClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        if (syntaxNode.IntoKeyword != default)
        {
            var intoKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(intoKeywordBuilder, syntaxNode.IntoKeyword);
            properties.Add($"\"intoKeyword\":{intoKeywordBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteLabeledStatementSyntax(StringBuilder builder, LabeledStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.ColonToken != default)
        {
            var colonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(colonTokenBuilder, syntaxNode.ColonToken);
            properties.Add($"\"colonToken\":{colonTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Statement != null)
        {
            var statementBuilder = new StringBuilder();
            WriteSyntaxNode(statementBuilder, syntaxNode.Statement);
            properties.Add($"\"statement\":{statementBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteLetClauseSyntax(StringBuilder builder, LetClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.EqualsToken != default)
        {
            var equalsTokenBuilder = new StringBuilder();
            WriteSyntaxToken(equalsTokenBuilder, syntaxNode.EqualsToken);
            properties.Add($"\"equalsToken\":{equalsTokenBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.LetKeyword != default)
        {
            var letKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(letKeywordBuilder, syntaxNode.LetKeyword);
            properties.Add($"\"letKeyword\":{letKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteLineDirectivePositionSyntax(StringBuilder builder, LineDirectivePositionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Character != default)
        {
            var characterBuilder = new StringBuilder();
            WriteSyntaxToken(characterBuilder, syntaxNode.Character);
            properties.Add($"\"character\":{characterBuilder}");
        }
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.CommaToken != default)
        {
            var commaTokenBuilder = new StringBuilder();
            WriteSyntaxToken(commaTokenBuilder, syntaxNode.CommaToken);
            properties.Add($"\"commaToken\":{commaTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Line != default)
        {
            var lineBuilder = new StringBuilder();
            WriteSyntaxToken(lineBuilder, syntaxNode.Line);
            properties.Add($"\"line\":{lineBuilder}");
        }
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteLineDirectiveTriviaSyntax(StringBuilder builder, LineDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.File != default)
        {
            var fileBuilder = new StringBuilder();
            WriteSyntaxToken(fileBuilder, syntaxNode.File);
            properties.Add($"\"file\":{fileBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Line != default)
        {
            var lineBuilder = new StringBuilder();
            WriteSyntaxToken(lineBuilder, syntaxNode.Line);
            properties.Add($"\"line\":{lineBuilder}");
        }
        if (syntaxNode.LineKeyword != default)
        {
            var lineKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(lineKeywordBuilder, syntaxNode.LineKeyword);
            properties.Add($"\"lineKeyword\":{lineKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteLineSpanDirectiveTriviaSyntax(StringBuilder builder, LineSpanDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CharacterOffset != default)
        {
            var characterOffsetBuilder = new StringBuilder();
            WriteSyntaxToken(characterOffsetBuilder, syntaxNode.CharacterOffset);
            properties.Add($"\"characterOffset\":{characterOffsetBuilder}");
        }
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.End != null)
        {
            var endBuilder = new StringBuilder();
            WriteLineDirectivePositionSyntax(endBuilder, syntaxNode.End);
            properties.Add($"\"end\":{endBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.File != default)
        {
            var fileBuilder = new StringBuilder();
            WriteSyntaxToken(fileBuilder, syntaxNode.File);
            properties.Add($"\"file\":{fileBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.LineKeyword != default)
        {
            var lineKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(lineKeywordBuilder, syntaxNode.LineKeyword);
            properties.Add($"\"lineKeyword\":{lineKeywordBuilder}");
        }
        if (syntaxNode.MinusToken != default)
        {
            var minusTokenBuilder = new StringBuilder();
            WriteSyntaxToken(minusTokenBuilder, syntaxNode.MinusToken);
            properties.Add($"\"minusToken\":{minusTokenBuilder}");
        }
        if (syntaxNode.Start != null)
        {
            var startBuilder = new StringBuilder();
            WriteLineDirectivePositionSyntax(startBuilder, syntaxNode.Start);
            properties.Add($"\"start\":{startBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteListPatternSyntax(StringBuilder builder, ListPatternSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseBracketToken != default)
        {
            var closeBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBracketTokenBuilder, syntaxNode.CloseBracketToken);
            properties.Add($"\"closeBracketToken\":{closeBracketTokenBuilder}");
        }
        if (syntaxNode.Designation != null)
        {
            var designationBuilder = new StringBuilder();
            WriteSyntaxNode(designationBuilder, syntaxNode.Designation);
            properties.Add($"\"designation\":{designationBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBracketToken != default)
        {
            var openBracketTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBracketTokenBuilder, syntaxNode.OpenBracketToken);
            properties.Add($"\"openBracketToken\":{openBracketTokenBuilder}");
        }
        var patterns = new List<string>();
        foreach (var node in syntaxNode.Patterns)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            patterns.Add(innerBuilder.ToString());
        }
        properties.Add($"\"patterns\":[{string.Join(",", patterns)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteLiteralExpressionSyntax(StringBuilder builder, LiteralExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Token != default)
        {
            var tokenBuilder = new StringBuilder();
            WriteSyntaxToken(tokenBuilder, syntaxNode.Token);
            properties.Add($"\"token\":{tokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteLoadDirectiveTriviaSyntax(StringBuilder builder, LoadDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.File != default)
        {
            var fileBuilder = new StringBuilder();
            WriteSyntaxToken(fileBuilder, syntaxNode.File);
            properties.Add($"\"file\":{fileBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.LoadKeyword != default)
        {
            var loadKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(loadKeywordBuilder, syntaxNode.LoadKeyword);
            properties.Add($"\"loadKeyword\":{loadKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteLocalDeclarationStatementSyntax(StringBuilder builder, LocalDeclarationStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.AwaitKeyword != default)
        {
            var awaitKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(awaitKeywordBuilder, syntaxNode.AwaitKeyword);
            properties.Add($"\"awaitKeyword\":{awaitKeywordBuilder}");
        }
        if (syntaxNode.Declaration != null)
        {
            var declarationBuilder = new StringBuilder();
            WriteVariableDeclarationSyntax(declarationBuilder, syntaxNode.Declaration);
            properties.Add($"\"declaration\":{declarationBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isConst", syntaxNode.IsConst));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.UsingKeyword != default)
        {
            var usingKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(usingKeywordBuilder, syntaxNode.UsingKeyword);
            properties.Add($"\"usingKeyword\":{usingKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteLocalFunctionStatementSyntax(StringBuilder builder, LocalFunctionStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Body != null)
        {
            var bodyBuilder = new StringBuilder();
            WriteBlockSyntax(bodyBuilder, syntaxNode.Body);
            properties.Add($"\"body\":{bodyBuilder}");
        }
        var constraintClauses = new List<string>();
        foreach (var node in syntaxNode.ConstraintClauses)
        {
            var innerBuilder = new StringBuilder();
            WriteTypeParameterConstraintClauseSyntax(innerBuilder, node);
            constraintClauses.Add(innerBuilder.ToString());
        }
        properties.Add($"\"constraintClauses\":[{string.Join(",", constraintClauses)}]");
        if (syntaxNode.ExpressionBody != null)
        {
            var expressionBodyBuilder = new StringBuilder();
            WriteArrowExpressionClauseSyntax(expressionBodyBuilder, syntaxNode.ExpressionBody);
            properties.Add($"\"expressionBody\":{expressionBodyBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.ParameterList != null)
        {
            var parameterListBuilder = new StringBuilder();
            WriteParameterListSyntax(parameterListBuilder, syntaxNode.ParameterList);
            properties.Add($"\"parameterList\":{parameterListBuilder}");
        }
        if (syntaxNode.ReturnType != null)
        {
            var returnTypeBuilder = new StringBuilder();
            WriteSyntaxNode(returnTypeBuilder, syntaxNode.ReturnType);
            properties.Add($"\"returnType\":{returnTypeBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.TypeParameterList != null)
        {
            var typeParameterListBuilder = new StringBuilder();
            WriteTypeParameterListSyntax(typeParameterListBuilder, syntaxNode.TypeParameterList);
            properties.Add($"\"typeParameterList\":{typeParameterListBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteLockStatementSyntax(StringBuilder builder, LockStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.LockKeyword != default)
        {
            var lockKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(lockKeywordBuilder, syntaxNode.LockKeyword);
            properties.Add($"\"lockKeyword\":{lockKeywordBuilder}");
        }
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.Statement != null)
        {
            var statementBuilder = new StringBuilder();
            WriteSyntaxNode(statementBuilder, syntaxNode.Statement);
            properties.Add($"\"statement\":{statementBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteMakeRefExpressionSyntax(StringBuilder builder, MakeRefExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteMemberAccessExpressionSyntax(StringBuilder builder, MemberAccessExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteSyntaxNode(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        if (syntaxNode.OperatorToken != default)
        {
            var operatorTokenBuilder = new StringBuilder();
            WriteSyntaxToken(operatorTokenBuilder, syntaxNode.OperatorToken);
            properties.Add($"\"operatorToken\":{operatorTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteMemberBindingExpressionSyntax(StringBuilder builder, MemberBindingExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteSyntaxNode(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        if (syntaxNode.OperatorToken != default)
        {
            var operatorTokenBuilder = new StringBuilder();
            WriteSyntaxToken(operatorTokenBuilder, syntaxNode.OperatorToken);
            properties.Add($"\"operatorToken\":{operatorTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteMethodDeclarationSyntax(StringBuilder builder, MethodDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteInt("arity", syntaxNode.Arity));
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Body != null)
        {
            var bodyBuilder = new StringBuilder();
            WriteBlockSyntax(bodyBuilder, syntaxNode.Body);
            properties.Add($"\"body\":{bodyBuilder}");
        }
        var constraintClauses = new List<string>();
        foreach (var node in syntaxNode.ConstraintClauses)
        {
            var innerBuilder = new StringBuilder();
            WriteTypeParameterConstraintClauseSyntax(innerBuilder, node);
            constraintClauses.Add(innerBuilder.ToString());
        }
        properties.Add($"\"constraintClauses\":[{string.Join(",", constraintClauses)}]");
        if (syntaxNode.ExplicitInterfaceSpecifier != null)
        {
            var explicitInterfaceSpecifierBuilder = new StringBuilder();
            WriteExplicitInterfaceSpecifierSyntax(explicitInterfaceSpecifierBuilder, syntaxNode.ExplicitInterfaceSpecifier);
            properties.Add($"\"explicitInterfaceSpecifier\":{explicitInterfaceSpecifierBuilder}");
        }
        if (syntaxNode.ExpressionBody != null)
        {
            var expressionBodyBuilder = new StringBuilder();
            WriteArrowExpressionClauseSyntax(expressionBodyBuilder, syntaxNode.ExpressionBody);
            properties.Add($"\"expressionBody\":{expressionBodyBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.ParameterList != null)
        {
            var parameterListBuilder = new StringBuilder();
            WriteParameterListSyntax(parameterListBuilder, syntaxNode.ParameterList);
            properties.Add($"\"parameterList\":{parameterListBuilder}");
        }
        if (syntaxNode.ReturnType != null)
        {
            var returnTypeBuilder = new StringBuilder();
            WriteSyntaxNode(returnTypeBuilder, syntaxNode.ReturnType);
            properties.Add($"\"returnType\":{returnTypeBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.TypeParameterList != null)
        {
            var typeParameterListBuilder = new StringBuilder();
            WriteTypeParameterListSyntax(typeParameterListBuilder, syntaxNode.TypeParameterList);
            properties.Add($"\"typeParameterList\":{typeParameterListBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteNameColonSyntax(StringBuilder builder, NameColonSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ColonToken != default)
        {
            var colonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(colonTokenBuilder, syntaxNode.ColonToken);
            properties.Add($"\"colonToken\":{colonTokenBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteIdentifierNameSyntax(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteNameEqualsSyntax(StringBuilder builder, NameEqualsSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.EqualsToken != default)
        {
            var equalsTokenBuilder = new StringBuilder();
            WriteSyntaxToken(equalsTokenBuilder, syntaxNode.EqualsToken);
            properties.Add($"\"equalsToken\":{equalsTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteIdentifierNameSyntax(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteNameMemberCrefSyntax(StringBuilder builder, NameMemberCrefSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteSyntaxNode(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        if (syntaxNode.Parameters != null)
        {
            var parametersBuilder = new StringBuilder();
            WriteCrefParameterListSyntax(parametersBuilder, syntaxNode.Parameters);
            properties.Add($"\"parameters\":{parametersBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteNamespaceDeclarationSyntax(StringBuilder builder, NamespaceDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.CloseBraceToken != default)
        {
            var closeBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBraceTokenBuilder, syntaxNode.CloseBraceToken);
            properties.Add($"\"closeBraceToken\":{closeBraceTokenBuilder}");
        }
        var externs = new List<string>();
        foreach (var node in syntaxNode.Externs)
        {
            var innerBuilder = new StringBuilder();
            WriteExternAliasDirectiveSyntax(innerBuilder, node);
            externs.Add(innerBuilder.ToString());
        }
        properties.Add($"\"externs\":[{string.Join(",", externs)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var members = new List<string>();
        foreach (var node in syntaxNode.Members)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            members.Add(innerBuilder.ToString());
        }
        properties.Add($"\"members\":[{string.Join(",", members)}]");
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteSyntaxNode(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        if (syntaxNode.NamespaceKeyword != default)
        {
            var namespaceKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(namespaceKeywordBuilder, syntaxNode.NamespaceKeyword);
            properties.Add($"\"namespaceKeyword\":{namespaceKeywordBuilder}");
        }
        if (syntaxNode.OpenBraceToken != default)
        {
            var openBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBraceTokenBuilder, syntaxNode.OpenBraceToken);
            properties.Add($"\"openBraceToken\":{openBraceTokenBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        var usings = new List<string>();
        foreach (var node in syntaxNode.Usings)
        {
            var innerBuilder = new StringBuilder();
            WriteUsingDirectiveSyntax(innerBuilder, node);
            usings.Add(innerBuilder.ToString());
        }
        properties.Add($"\"usings\":[{string.Join(",", usings)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteNullableDirectiveTriviaSyntax(StringBuilder builder, NullableDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.NullableKeyword != default)
        {
            var nullableKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(nullableKeywordBuilder, syntaxNode.NullableKeyword);
            properties.Add($"\"nullableKeyword\":{nullableKeywordBuilder}");
        }
        if (syntaxNode.SettingToken != default)
        {
            var settingTokenBuilder = new StringBuilder();
            WriteSyntaxToken(settingTokenBuilder, syntaxNode.SettingToken);
            properties.Add($"\"settingToken\":{settingTokenBuilder}");
        }
        if (syntaxNode.TargetToken != default)
        {
            var targetTokenBuilder = new StringBuilder();
            WriteSyntaxToken(targetTokenBuilder, syntaxNode.TargetToken);
            properties.Add($"\"targetToken\":{targetTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteNullableTypeSyntax(StringBuilder builder, NullableTypeSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ElementType != null)
        {
            var elementTypeBuilder = new StringBuilder();
            WriteSyntaxNode(elementTypeBuilder, syntaxNode.ElementType);
            properties.Add($"\"elementType\":{elementTypeBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        properties.Add(WriteBoolean("isNint", syntaxNode.IsNint));
        properties.Add(WriteBoolean("isNotNull", syntaxNode.IsNotNull));
        properties.Add(WriteBoolean("isNuint", syntaxNode.IsNuint));
        properties.Add(WriteBoolean("isUnmanaged", syntaxNode.IsUnmanaged));
        properties.Add(WriteBoolean("isVar", syntaxNode.IsVar));
        if (syntaxNode.QuestionToken != default)
        {
            var questionTokenBuilder = new StringBuilder();
            WriteSyntaxToken(questionTokenBuilder, syntaxNode.QuestionToken);
            properties.Add($"\"questionToken\":{questionTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteObjectCreationExpressionSyntax(StringBuilder builder, ObjectCreationExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ArgumentList != null)
        {
            var argumentListBuilder = new StringBuilder();
            WriteArgumentListSyntax(argumentListBuilder, syntaxNode.ArgumentList);
            properties.Add($"\"argumentList\":{argumentListBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Initializer != null)
        {
            var initializerBuilder = new StringBuilder();
            WriteInitializerExpressionSyntax(initializerBuilder, syntaxNode.Initializer);
            properties.Add($"\"initializer\":{initializerBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.NewKeyword != default)
        {
            var newKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(newKeywordBuilder, syntaxNode.NewKeyword);
            properties.Add($"\"newKeyword\":{newKeywordBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteOmittedArraySizeExpressionSyntax(StringBuilder builder, OmittedArraySizeExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OmittedArraySizeExpressionToken != default)
        {
            var omittedArraySizeExpressionTokenBuilder = new StringBuilder();
            WriteSyntaxToken(omittedArraySizeExpressionTokenBuilder, syntaxNode.OmittedArraySizeExpressionToken);
            properties.Add($"\"omittedArraySizeExpressionToken\":{omittedArraySizeExpressionTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteOmittedTypeArgumentSyntax(StringBuilder builder, OmittedTypeArgumentSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        properties.Add(WriteBoolean("isNint", syntaxNode.IsNint));
        properties.Add(WriteBoolean("isNotNull", syntaxNode.IsNotNull));
        properties.Add(WriteBoolean("isNuint", syntaxNode.IsNuint));
        properties.Add(WriteBoolean("isUnmanaged", syntaxNode.IsUnmanaged));
        properties.Add(WriteBoolean("isVar", syntaxNode.IsVar));
        if (syntaxNode.OmittedTypeArgumentToken != default)
        {
            var omittedTypeArgumentTokenBuilder = new StringBuilder();
            WriteSyntaxToken(omittedTypeArgumentTokenBuilder, syntaxNode.OmittedTypeArgumentToken);
            properties.Add($"\"omittedTypeArgumentToken\":{omittedTypeArgumentTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteOperatorDeclarationSyntax(StringBuilder builder, OperatorDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Body != null)
        {
            var bodyBuilder = new StringBuilder();
            WriteBlockSyntax(bodyBuilder, syntaxNode.Body);
            properties.Add($"\"body\":{bodyBuilder}");
        }
        if (syntaxNode.CheckedKeyword != default)
        {
            var checkedKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(checkedKeywordBuilder, syntaxNode.CheckedKeyword);
            properties.Add($"\"checkedKeyword\":{checkedKeywordBuilder}");
        }
        if (syntaxNode.ExplicitInterfaceSpecifier != null)
        {
            var explicitInterfaceSpecifierBuilder = new StringBuilder();
            WriteExplicitInterfaceSpecifierSyntax(explicitInterfaceSpecifierBuilder, syntaxNode.ExplicitInterfaceSpecifier);
            properties.Add($"\"explicitInterfaceSpecifier\":{explicitInterfaceSpecifierBuilder}");
        }
        if (syntaxNode.ExpressionBody != null)
        {
            var expressionBodyBuilder = new StringBuilder();
            WriteArrowExpressionClauseSyntax(expressionBodyBuilder, syntaxNode.ExpressionBody);
            properties.Add($"\"expressionBody\":{expressionBodyBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.OperatorKeyword != default)
        {
            var operatorKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(operatorKeywordBuilder, syntaxNode.OperatorKeyword);
            properties.Add($"\"operatorKeyword\":{operatorKeywordBuilder}");
        }
        if (syntaxNode.OperatorToken != default)
        {
            var operatorTokenBuilder = new StringBuilder();
            WriteSyntaxToken(operatorTokenBuilder, syntaxNode.OperatorToken);
            properties.Add($"\"operatorToken\":{operatorTokenBuilder}");
        }
        if (syntaxNode.ParameterList != null)
        {
            var parameterListBuilder = new StringBuilder();
            WriteParameterListSyntax(parameterListBuilder, syntaxNode.ParameterList);
            properties.Add($"\"parameterList\":{parameterListBuilder}");
        }
        if (syntaxNode.ReturnType != null)
        {
            var returnTypeBuilder = new StringBuilder();
            WriteSyntaxNode(returnTypeBuilder, syntaxNode.ReturnType);
            properties.Add($"\"returnType\":{returnTypeBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteOperatorMemberCrefSyntax(StringBuilder builder, OperatorMemberCrefSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CheckedKeyword != default)
        {
            var checkedKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(checkedKeywordBuilder, syntaxNode.CheckedKeyword);
            properties.Add($"\"checkedKeyword\":{checkedKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OperatorKeyword != default)
        {
            var operatorKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(operatorKeywordBuilder, syntaxNode.OperatorKeyword);
            properties.Add($"\"operatorKeyword\":{operatorKeywordBuilder}");
        }
        if (syntaxNode.OperatorToken != default)
        {
            var operatorTokenBuilder = new StringBuilder();
            WriteSyntaxToken(operatorTokenBuilder, syntaxNode.OperatorToken);
            properties.Add($"\"operatorToken\":{operatorTokenBuilder}");
        }
        if (syntaxNode.Parameters != null)
        {
            var parametersBuilder = new StringBuilder();
            WriteCrefParameterListSyntax(parametersBuilder, syntaxNode.Parameters);
            properties.Add($"\"parameters\":{parametersBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteOrderByClauseSyntax(StringBuilder builder, OrderByClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OrderByKeyword != default)
        {
            var orderByKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(orderByKeywordBuilder, syntaxNode.OrderByKeyword);
            properties.Add($"\"orderByKeyword\":{orderByKeywordBuilder}");
        }
        var orderings = new List<string>();
        foreach (var node in syntaxNode.Orderings)
        {
            var innerBuilder = new StringBuilder();
            WriteOrderingSyntax(innerBuilder, node);
            orderings.Add(innerBuilder.ToString());
        }
        properties.Add($"\"orderings\":[{string.Join(",", orderings)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteOrderingSyntax(StringBuilder builder, OrderingSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.AscendingOrDescendingKeyword != default)
        {
            var ascendingOrDescendingKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(ascendingOrDescendingKeywordBuilder, syntaxNode.AscendingOrDescendingKeyword);
            properties.Add($"\"ascendingOrDescendingKeyword\":{ascendingOrDescendingKeywordBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteParameterListSyntax(StringBuilder builder, ParameterListSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        var parameters = new List<string>();
        foreach (var node in syntaxNode.Parameters)
        {
            var innerBuilder = new StringBuilder();
            WriteParameterSyntax(innerBuilder, node);
            parameters.Add(innerBuilder.ToString());
        }
        properties.Add($"\"parameters\":[{string.Join(",", parameters)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteParameterSyntax(StringBuilder builder, ParameterSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Default != null)
        {
            var defaultBuilder = new StringBuilder();
            WriteEqualsValueClauseSyntax(defaultBuilder, syntaxNode.Default);
            properties.Add($"\"default\":{defaultBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteParenthesizedExpressionSyntax(StringBuilder builder, ParenthesizedExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteParenthesizedLambdaExpressionSyntax(StringBuilder builder, ParenthesizedLambdaExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ArrowToken != default)
        {
            var arrowTokenBuilder = new StringBuilder();
            WriteSyntaxToken(arrowTokenBuilder, syntaxNode.ArrowToken);
            properties.Add($"\"arrowToken\":{arrowTokenBuilder}");
        }
        if (syntaxNode.AsyncKeyword != default)
        {
            var asyncKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(asyncKeywordBuilder, syntaxNode.AsyncKeyword);
            properties.Add($"\"asyncKeyword\":{asyncKeywordBuilder}");
        }
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Block != null)
        {
            var blockBuilder = new StringBuilder();
            WriteBlockSyntax(blockBuilder, syntaxNode.Block);
            properties.Add($"\"block\":{blockBuilder}");
        }
        if (syntaxNode.Body != null)
        {
            var bodyBuilder = new StringBuilder();
            WriteSyntaxNode(bodyBuilder, syntaxNode.Body);
            properties.Add($"\"body\":{bodyBuilder}");
        }
        if (syntaxNode.ExpressionBody != null)
        {
            var expressionBodyBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBodyBuilder, syntaxNode.ExpressionBody);
            properties.Add($"\"expressionBody\":{expressionBodyBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.ParameterList != null)
        {
            var parameterListBuilder = new StringBuilder();
            WriteParameterListSyntax(parameterListBuilder, syntaxNode.ParameterList);
            properties.Add($"\"parameterList\":{parameterListBuilder}");
        }
        if (syntaxNode.ReturnType != null)
        {
            var returnTypeBuilder = new StringBuilder();
            WriteSyntaxNode(returnTypeBuilder, syntaxNode.ReturnType);
            properties.Add($"\"returnType\":{returnTypeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteParenthesizedPatternSyntax(StringBuilder builder, ParenthesizedPatternSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.Pattern != null)
        {
            var patternBuilder = new StringBuilder();
            WriteSyntaxNode(patternBuilder, syntaxNode.Pattern);
            properties.Add($"\"pattern\":{patternBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteParenthesizedVariableDesignationSyntax(StringBuilder builder, ParenthesizedVariableDesignationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        var variables = new List<string>();
        foreach (var node in syntaxNode.Variables)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            variables.Add(innerBuilder.ToString());
        }
        properties.Add($"\"variables\":[{string.Join(",", variables)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WritePointerTypeSyntax(StringBuilder builder, PointerTypeSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.AsteriskToken != default)
        {
            var asteriskTokenBuilder = new StringBuilder();
            WriteSyntaxToken(asteriskTokenBuilder, syntaxNode.AsteriskToken);
            properties.Add($"\"asteriskToken\":{asteriskTokenBuilder}");
        }
        if (syntaxNode.ElementType != null)
        {
            var elementTypeBuilder = new StringBuilder();
            WriteSyntaxNode(elementTypeBuilder, syntaxNode.ElementType);
            properties.Add($"\"elementType\":{elementTypeBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        properties.Add(WriteBoolean("isNint", syntaxNode.IsNint));
        properties.Add(WriteBoolean("isNotNull", syntaxNode.IsNotNull));
        properties.Add(WriteBoolean("isNuint", syntaxNode.IsNuint));
        properties.Add(WriteBoolean("isUnmanaged", syntaxNode.IsUnmanaged));
        properties.Add(WriteBoolean("isVar", syntaxNode.IsVar));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WritePositionalPatternClauseSyntax(StringBuilder builder, PositionalPatternClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        var subpatterns = new List<string>();
        foreach (var node in syntaxNode.Subpatterns)
        {
            var innerBuilder = new StringBuilder();
            WriteSubpatternSyntax(innerBuilder, node);
            subpatterns.Add(innerBuilder.ToString());
        }
        properties.Add($"\"subpatterns\":[{string.Join(",", subpatterns)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WritePostfixUnaryExpressionSyntax(StringBuilder builder, PostfixUnaryExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Operand != null)
        {
            var operandBuilder = new StringBuilder();
            WriteSyntaxNode(operandBuilder, syntaxNode.Operand);
            properties.Add($"\"operand\":{operandBuilder}");
        }
        if (syntaxNode.OperatorToken != default)
        {
            var operatorTokenBuilder = new StringBuilder();
            WriteSyntaxToken(operatorTokenBuilder, syntaxNode.OperatorToken);
            properties.Add($"\"operatorToken\":{operatorTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WritePragmaChecksumDirectiveTriviaSyntax(StringBuilder builder, PragmaChecksumDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Bytes != default)
        {
            var bytesBuilder = new StringBuilder();
            WriteSyntaxToken(bytesBuilder, syntaxNode.Bytes);
            properties.Add($"\"bytes\":{bytesBuilder}");
        }
        if (syntaxNode.ChecksumKeyword != default)
        {
            var checksumKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(checksumKeywordBuilder, syntaxNode.ChecksumKeyword);
            properties.Add($"\"checksumKeyword\":{checksumKeywordBuilder}");
        }
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.File != default)
        {
            var fileBuilder = new StringBuilder();
            WriteSyntaxToken(fileBuilder, syntaxNode.File);
            properties.Add($"\"file\":{fileBuilder}");
        }
        if (syntaxNode.Guid != default)
        {
            var guidBuilder = new StringBuilder();
            WriteSyntaxToken(guidBuilder, syntaxNode.Guid);
            properties.Add($"\"guid\":{guidBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.PragmaKeyword != default)
        {
            var pragmaKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(pragmaKeywordBuilder, syntaxNode.PragmaKeyword);
            properties.Add($"\"pragmaKeyword\":{pragmaKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WritePragmaWarningDirectiveTriviaSyntax(StringBuilder builder, PragmaWarningDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.DisableOrRestoreKeyword != default)
        {
            var disableOrRestoreKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(disableOrRestoreKeywordBuilder, syntaxNode.DisableOrRestoreKeyword);
            properties.Add($"\"disableOrRestoreKeyword\":{disableOrRestoreKeywordBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        var errorCodes = new List<string>();
        foreach (var node in syntaxNode.ErrorCodes)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            errorCodes.Add(innerBuilder.ToString());
        }
        properties.Add($"\"errorCodes\":[{string.Join(",", errorCodes)}]");
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.PragmaKeyword != default)
        {
            var pragmaKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(pragmaKeywordBuilder, syntaxNode.PragmaKeyword);
            properties.Add($"\"pragmaKeyword\":{pragmaKeywordBuilder}");
        }
        if (syntaxNode.WarningKeyword != default)
        {
            var warningKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(warningKeywordBuilder, syntaxNode.WarningKeyword);
            properties.Add($"\"warningKeyword\":{warningKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WritePredefinedTypeSyntax(StringBuilder builder, PredefinedTypeSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        properties.Add(WriteBoolean("isNint", syntaxNode.IsNint));
        properties.Add(WriteBoolean("isNotNull", syntaxNode.IsNotNull));
        properties.Add(WriteBoolean("isNuint", syntaxNode.IsNuint));
        properties.Add(WriteBoolean("isUnmanaged", syntaxNode.IsUnmanaged));
        properties.Add(WriteBoolean("isVar", syntaxNode.IsVar));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WritePrefixUnaryExpressionSyntax(StringBuilder builder, PrefixUnaryExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Operand != null)
        {
            var operandBuilder = new StringBuilder();
            WriteSyntaxNode(operandBuilder, syntaxNode.Operand);
            properties.Add($"\"operand\":{operandBuilder}");
        }
        if (syntaxNode.OperatorToken != default)
        {
            var operatorTokenBuilder = new StringBuilder();
            WriteSyntaxToken(operatorTokenBuilder, syntaxNode.OperatorToken);
            properties.Add($"\"operatorToken\":{operatorTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WritePrimaryConstructorBaseTypeSyntax(StringBuilder builder, PrimaryConstructorBaseTypeSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ArgumentList != null)
        {
            var argumentListBuilder = new StringBuilder();
            WriteArgumentListSyntax(argumentListBuilder, syntaxNode.ArgumentList);
            properties.Add($"\"argumentList\":{argumentListBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WritePropertyDeclarationSyntax(StringBuilder builder, PropertyDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.AccessorList != null)
        {
            var accessorListBuilder = new StringBuilder();
            WriteAccessorListSyntax(accessorListBuilder, syntaxNode.AccessorList);
            properties.Add($"\"accessorList\":{accessorListBuilder}");
        }
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.ExplicitInterfaceSpecifier != null)
        {
            var explicitInterfaceSpecifierBuilder = new StringBuilder();
            WriteExplicitInterfaceSpecifierSyntax(explicitInterfaceSpecifierBuilder, syntaxNode.ExplicitInterfaceSpecifier);
            properties.Add($"\"explicitInterfaceSpecifier\":{explicitInterfaceSpecifierBuilder}");
        }
        if (syntaxNode.ExpressionBody != null)
        {
            var expressionBodyBuilder = new StringBuilder();
            WriteArrowExpressionClauseSyntax(expressionBodyBuilder, syntaxNode.ExpressionBody);
            properties.Add($"\"expressionBody\":{expressionBodyBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        if (syntaxNode.Initializer != null)
        {
            var initializerBuilder = new StringBuilder();
            WriteEqualsValueClauseSyntax(initializerBuilder, syntaxNode.Initializer);
            properties.Add($"\"initializer\":{initializerBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WritePropertyPatternClauseSyntax(StringBuilder builder, PropertyPatternClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseBraceToken != default)
        {
            var closeBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBraceTokenBuilder, syntaxNode.CloseBraceToken);
            properties.Add($"\"closeBraceToken\":{closeBraceTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBraceToken != default)
        {
            var openBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBraceTokenBuilder, syntaxNode.OpenBraceToken);
            properties.Add($"\"openBraceToken\":{openBraceTokenBuilder}");
        }
        var subpatterns = new List<string>();
        foreach (var node in syntaxNode.Subpatterns)
        {
            var innerBuilder = new StringBuilder();
            WriteSubpatternSyntax(innerBuilder, node);
            subpatterns.Add(innerBuilder.ToString());
        }
        properties.Add($"\"subpatterns\":[{string.Join(",", subpatterns)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteQualifiedCrefSyntax(StringBuilder builder, QualifiedCrefSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Container != null)
        {
            var containerBuilder = new StringBuilder();
            WriteSyntaxNode(containerBuilder, syntaxNode.Container);
            properties.Add($"\"container\":{containerBuilder}");
        }
        if (syntaxNode.DotToken != default)
        {
            var dotTokenBuilder = new StringBuilder();
            WriteSyntaxToken(dotTokenBuilder, syntaxNode.DotToken);
            properties.Add($"\"dotToken\":{dotTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Member != null)
        {
            var memberBuilder = new StringBuilder();
            WriteSyntaxNode(memberBuilder, syntaxNode.Member);
            properties.Add($"\"member\":{memberBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteQualifiedNameSyntax(StringBuilder builder, QualifiedNameSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteInt("arity", syntaxNode.Arity));
        if (syntaxNode.DotToken != default)
        {
            var dotTokenBuilder = new StringBuilder();
            WriteSyntaxToken(dotTokenBuilder, syntaxNode.DotToken);
            properties.Add($"\"dotToken\":{dotTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        properties.Add(WriteBoolean("isNint", syntaxNode.IsNint));
        properties.Add(WriteBoolean("isNotNull", syntaxNode.IsNotNull));
        properties.Add(WriteBoolean("isNuint", syntaxNode.IsNuint));
        properties.Add(WriteBoolean("isUnmanaged", syntaxNode.IsUnmanaged));
        properties.Add(WriteBoolean("isVar", syntaxNode.IsVar));
        if (syntaxNode.Left != null)
        {
            var leftBuilder = new StringBuilder();
            WriteSyntaxNode(leftBuilder, syntaxNode.Left);
            properties.Add($"\"left\":{leftBuilder}");
        }
        if (syntaxNode.Right != null)
        {
            var rightBuilder = new StringBuilder();
            WriteSyntaxNode(rightBuilder, syntaxNode.Right);
            properties.Add($"\"right\":{rightBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteQueryBodySyntax(StringBuilder builder, QueryBodySyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var clauses = new List<string>();
        foreach (var node in syntaxNode.Clauses)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            clauses.Add(innerBuilder.ToString());
        }
        properties.Add($"\"clauses\":[{string.Join(",", clauses)}]");
        if (syntaxNode.Continuation != null)
        {
            var continuationBuilder = new StringBuilder();
            WriteQueryContinuationSyntax(continuationBuilder, syntaxNode.Continuation);
            properties.Add($"\"continuation\":{continuationBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.SelectOrGroup != null)
        {
            var selectOrGroupBuilder = new StringBuilder();
            WriteSyntaxNode(selectOrGroupBuilder, syntaxNode.SelectOrGroup);
            properties.Add($"\"selectOrGroup\":{selectOrGroupBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteQueryContinuationSyntax(StringBuilder builder, QueryContinuationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Body != null)
        {
            var bodyBuilder = new StringBuilder();
            WriteQueryBodySyntax(bodyBuilder, syntaxNode.Body);
            properties.Add($"\"body\":{bodyBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        if (syntaxNode.IntoKeyword != default)
        {
            var intoKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(intoKeywordBuilder, syntaxNode.IntoKeyword);
            properties.Add($"\"intoKeyword\":{intoKeywordBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteQueryExpressionSyntax(StringBuilder builder, QueryExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Body != null)
        {
            var bodyBuilder = new StringBuilder();
            WriteQueryBodySyntax(bodyBuilder, syntaxNode.Body);
            properties.Add($"\"body\":{bodyBuilder}");
        }
        if (syntaxNode.FromClause != null)
        {
            var fromClauseBuilder = new StringBuilder();
            WriteFromClauseSyntax(fromClauseBuilder, syntaxNode.FromClause);
            properties.Add($"\"fromClause\":{fromClauseBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteRangeExpressionSyntax(StringBuilder builder, RangeExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.LeftOperand != null)
        {
            var leftOperandBuilder = new StringBuilder();
            WriteSyntaxNode(leftOperandBuilder, syntaxNode.LeftOperand);
            properties.Add($"\"leftOperand\":{leftOperandBuilder}");
        }
        if (syntaxNode.OperatorToken != default)
        {
            var operatorTokenBuilder = new StringBuilder();
            WriteSyntaxToken(operatorTokenBuilder, syntaxNode.OperatorToken);
            properties.Add($"\"operatorToken\":{operatorTokenBuilder}");
        }
        if (syntaxNode.RightOperand != null)
        {
            var rightOperandBuilder = new StringBuilder();
            WriteSyntaxNode(rightOperandBuilder, syntaxNode.RightOperand);
            properties.Add($"\"rightOperand\":{rightOperandBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteRecordDeclarationSyntax(StringBuilder builder, RecordDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteInt("arity", syntaxNode.Arity));
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.BaseList != null)
        {
            var baseListBuilder = new StringBuilder();
            WriteBaseListSyntax(baseListBuilder, syntaxNode.BaseList);
            properties.Add($"\"baseList\":{baseListBuilder}");
        }
        if (syntaxNode.ClassOrStructKeyword != default)
        {
            var classOrStructKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(classOrStructKeywordBuilder, syntaxNode.ClassOrStructKeyword);
            properties.Add($"\"classOrStructKeyword\":{classOrStructKeywordBuilder}");
        }
        if (syntaxNode.CloseBraceToken != default)
        {
            var closeBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBraceTokenBuilder, syntaxNode.CloseBraceToken);
            properties.Add($"\"closeBraceToken\":{closeBraceTokenBuilder}");
        }
        var constraintClauses = new List<string>();
        foreach (var node in syntaxNode.ConstraintClauses)
        {
            var innerBuilder = new StringBuilder();
            WriteTypeParameterConstraintClauseSyntax(innerBuilder, node);
            constraintClauses.Add(innerBuilder.ToString());
        }
        properties.Add($"\"constraintClauses\":[{string.Join(",", constraintClauses)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        var members = new List<string>();
        foreach (var node in syntaxNode.Members)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            members.Add(innerBuilder.ToString());
        }
        properties.Add($"\"members\":[{string.Join(",", members)}]");
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.OpenBraceToken != default)
        {
            var openBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBraceTokenBuilder, syntaxNode.OpenBraceToken);
            properties.Add($"\"openBraceToken\":{openBraceTokenBuilder}");
        }
        if (syntaxNode.ParameterList != null)
        {
            var parameterListBuilder = new StringBuilder();
            WriteParameterListSyntax(parameterListBuilder, syntaxNode.ParameterList);
            properties.Add($"\"parameterList\":{parameterListBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.TypeParameterList != null)
        {
            var typeParameterListBuilder = new StringBuilder();
            WriteTypeParameterListSyntax(typeParameterListBuilder, syntaxNode.TypeParameterList);
            properties.Add($"\"typeParameterList\":{typeParameterListBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteRecursivePatternSyntax(StringBuilder builder, RecursivePatternSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Designation != null)
        {
            var designationBuilder = new StringBuilder();
            WriteSyntaxNode(designationBuilder, syntaxNode.Designation);
            properties.Add($"\"designation\":{designationBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.PositionalPatternClause != null)
        {
            var positionalPatternClauseBuilder = new StringBuilder();
            WritePositionalPatternClauseSyntax(positionalPatternClauseBuilder, syntaxNode.PositionalPatternClause);
            properties.Add($"\"positionalPatternClause\":{positionalPatternClauseBuilder}");
        }
        if (syntaxNode.PropertyPatternClause != null)
        {
            var propertyPatternClauseBuilder = new StringBuilder();
            WritePropertyPatternClauseSyntax(propertyPatternClauseBuilder, syntaxNode.PropertyPatternClause);
            properties.Add($"\"propertyPatternClause\":{propertyPatternClauseBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteReferenceDirectiveTriviaSyntax(StringBuilder builder, ReferenceDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.File != default)
        {
            var fileBuilder = new StringBuilder();
            WriteSyntaxToken(fileBuilder, syntaxNode.File);
            properties.Add($"\"file\":{fileBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.ReferenceKeyword != default)
        {
            var referenceKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(referenceKeywordBuilder, syntaxNode.ReferenceKeyword);
            properties.Add($"\"referenceKeyword\":{referenceKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteRefExpressionSyntax(StringBuilder builder, RefExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.RefKeyword != default)
        {
            var refKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(refKeywordBuilder, syntaxNode.RefKeyword);
            properties.Add($"\"refKeyword\":{refKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteRefStructConstraintSyntax(StringBuilder builder, RefStructConstraintSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.RefKeyword != default)
        {
            var refKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(refKeywordBuilder, syntaxNode.RefKeyword);
            properties.Add($"\"refKeyword\":{refKeywordBuilder}");
        }
        if (syntaxNode.StructKeyword != default)
        {
            var structKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(structKeywordBuilder, syntaxNode.StructKeyword);
            properties.Add($"\"structKeyword\":{structKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteRefTypeExpressionSyntax(StringBuilder builder, RefTypeExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteRefTypeSyntax(StringBuilder builder, RefTypeSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        properties.Add(WriteBoolean("isNint", syntaxNode.IsNint));
        properties.Add(WriteBoolean("isNotNull", syntaxNode.IsNotNull));
        properties.Add(WriteBoolean("isNuint", syntaxNode.IsNuint));
        properties.Add(WriteBoolean("isUnmanaged", syntaxNode.IsUnmanaged));
        properties.Add(WriteBoolean("isVar", syntaxNode.IsVar));
        if (syntaxNode.ReadOnlyKeyword != default)
        {
            var readOnlyKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(readOnlyKeywordBuilder, syntaxNode.ReadOnlyKeyword);
            properties.Add($"\"readOnlyKeyword\":{readOnlyKeywordBuilder}");
        }
        if (syntaxNode.RefKeyword != default)
        {
            var refKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(refKeywordBuilder, syntaxNode.RefKeyword);
            properties.Add($"\"refKeyword\":{refKeywordBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteRefValueExpressionSyntax(StringBuilder builder, RefValueExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Comma != default)
        {
            var commaBuilder = new StringBuilder();
            WriteSyntaxToken(commaBuilder, syntaxNode.Comma);
            properties.Add($"\"comma\":{commaBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteRegionDirectiveTriviaSyntax(StringBuilder builder, RegionDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.RegionKeyword != default)
        {
            var regionKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(regionKeywordBuilder, syntaxNode.RegionKeyword);
            properties.Add($"\"regionKeyword\":{regionKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteRelationalPatternSyntax(StringBuilder builder, RelationalPatternSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OperatorToken != default)
        {
            var operatorTokenBuilder = new StringBuilder();
            WriteSyntaxToken(operatorTokenBuilder, syntaxNode.OperatorToken);
            properties.Add($"\"operatorToken\":{operatorTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteReturnStatementSyntax(StringBuilder builder, ReturnStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.ReturnKeyword != default)
        {
            var returnKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(returnKeywordBuilder, syntaxNode.ReturnKeyword);
            properties.Add($"\"returnKeyword\":{returnKeywordBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteScopedTypeSyntax(StringBuilder builder, ScopedTypeSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        properties.Add(WriteBoolean("isNint", syntaxNode.IsNint));
        properties.Add(WriteBoolean("isNotNull", syntaxNode.IsNotNull));
        properties.Add(WriteBoolean("isNuint", syntaxNode.IsNuint));
        properties.Add(WriteBoolean("isUnmanaged", syntaxNode.IsUnmanaged));
        properties.Add(WriteBoolean("isVar", syntaxNode.IsVar));
        if (syntaxNode.ScopedKeyword != default)
        {
            var scopedKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(scopedKeywordBuilder, syntaxNode.ScopedKeyword);
            properties.Add($"\"scopedKeyword\":{scopedKeywordBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteSelectClauseSyntax(StringBuilder builder, SelectClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.SelectKeyword != default)
        {
            var selectKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(selectKeywordBuilder, syntaxNode.SelectKeyword);
            properties.Add($"\"selectKeyword\":{selectKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteShebangDirectiveTriviaSyntax(StringBuilder builder, ShebangDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.ExclamationToken != default)
        {
            var exclamationTokenBuilder = new StringBuilder();
            WriteSyntaxToken(exclamationTokenBuilder, syntaxNode.ExclamationToken);
            properties.Add($"\"exclamationToken\":{exclamationTokenBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteSimpleBaseTypeSyntax(StringBuilder builder, SimpleBaseTypeSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteSimpleLambdaExpressionSyntax(StringBuilder builder, SimpleLambdaExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ArrowToken != default)
        {
            var arrowTokenBuilder = new StringBuilder();
            WriteSyntaxToken(arrowTokenBuilder, syntaxNode.ArrowToken);
            properties.Add($"\"arrowToken\":{arrowTokenBuilder}");
        }
        if (syntaxNode.AsyncKeyword != default)
        {
            var asyncKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(asyncKeywordBuilder, syntaxNode.AsyncKeyword);
            properties.Add($"\"asyncKeyword\":{asyncKeywordBuilder}");
        }
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Block != null)
        {
            var blockBuilder = new StringBuilder();
            WriteBlockSyntax(blockBuilder, syntaxNode.Block);
            properties.Add($"\"block\":{blockBuilder}");
        }
        if (syntaxNode.Body != null)
        {
            var bodyBuilder = new StringBuilder();
            WriteSyntaxNode(bodyBuilder, syntaxNode.Body);
            properties.Add($"\"body\":{bodyBuilder}");
        }
        if (syntaxNode.ExpressionBody != null)
        {
            var expressionBodyBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBodyBuilder, syntaxNode.ExpressionBody);
            properties.Add($"\"expressionBody\":{expressionBodyBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.Parameter != null)
        {
            var parameterBuilder = new StringBuilder();
            WriteParameterSyntax(parameterBuilder, syntaxNode.Parameter);
            properties.Add($"\"parameter\":{parameterBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteSingleVariableDesignationSyntax(StringBuilder builder, SingleVariableDesignationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteSizeOfExpressionSyntax(StringBuilder builder, SizeOfExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteSkippedTokensTriviaSyntax(StringBuilder builder, SkippedTokensTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var tokens = new List<string>();
        foreach (var node in syntaxNode.Tokens)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            tokens.Add(innerBuilder.ToString());
        }
        properties.Add($"\"tokens\":[{string.Join(",", tokens)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteSlicePatternSyntax(StringBuilder builder, SlicePatternSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DotDotToken != default)
        {
            var dotDotTokenBuilder = new StringBuilder();
            WriteSyntaxToken(dotDotTokenBuilder, syntaxNode.DotDotToken);
            properties.Add($"\"dotDotToken\":{dotDotTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Pattern != null)
        {
            var patternBuilder = new StringBuilder();
            WriteSyntaxNode(patternBuilder, syntaxNode.Pattern);
            properties.Add($"\"pattern\":{patternBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteSpreadElementSyntax(StringBuilder builder, SpreadElementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OperatorToken != default)
        {
            var operatorTokenBuilder = new StringBuilder();
            WriteSyntaxToken(operatorTokenBuilder, syntaxNode.OperatorToken);
            properties.Add($"\"operatorToken\":{operatorTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteStackAllocArrayCreationExpressionSyntax(StringBuilder builder, StackAllocArrayCreationExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Initializer != null)
        {
            var initializerBuilder = new StringBuilder();
            WriteInitializerExpressionSyntax(initializerBuilder, syntaxNode.Initializer);
            properties.Add($"\"initializer\":{initializerBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.StackAllocKeyword != default)
        {
            var stackAllocKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(stackAllocKeywordBuilder, syntaxNode.StackAllocKeyword);
            properties.Add($"\"stackAllocKeyword\":{stackAllocKeywordBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteStructDeclarationSyntax(StringBuilder builder, StructDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteInt("arity", syntaxNode.Arity));
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.BaseList != null)
        {
            var baseListBuilder = new StringBuilder();
            WriteBaseListSyntax(baseListBuilder, syntaxNode.BaseList);
            properties.Add($"\"baseList\":{baseListBuilder}");
        }
        if (syntaxNode.CloseBraceToken != default)
        {
            var closeBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBraceTokenBuilder, syntaxNode.CloseBraceToken);
            properties.Add($"\"closeBraceToken\":{closeBraceTokenBuilder}");
        }
        var constraintClauses = new List<string>();
        foreach (var node in syntaxNode.ConstraintClauses)
        {
            var innerBuilder = new StringBuilder();
            WriteTypeParameterConstraintClauseSyntax(innerBuilder, node);
            constraintClauses.Add(innerBuilder.ToString());
        }
        properties.Add($"\"constraintClauses\":[{string.Join(",", constraintClauses)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        var members = new List<string>();
        foreach (var node in syntaxNode.Members)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            members.Add(innerBuilder.ToString());
        }
        properties.Add($"\"members\":[{string.Join(",", members)}]");
        var modifiers = new List<string>();
        foreach (var node in syntaxNode.Modifiers)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            modifiers.Add(innerBuilder.ToString());
        }
        properties.Add($"\"modifiers\":[{string.Join(",", modifiers)}]");
        if (syntaxNode.OpenBraceToken != default)
        {
            var openBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBraceTokenBuilder, syntaxNode.OpenBraceToken);
            properties.Add($"\"openBraceToken\":{openBraceTokenBuilder}");
        }
        if (syntaxNode.ParameterList != null)
        {
            var parameterListBuilder = new StringBuilder();
            WriteParameterListSyntax(parameterListBuilder, syntaxNode.ParameterList);
            properties.Add($"\"parameterList\":{parameterListBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.TypeParameterList != null)
        {
            var typeParameterListBuilder = new StringBuilder();
            WriteTypeParameterListSyntax(typeParameterListBuilder, syntaxNode.TypeParameterList);
            properties.Add($"\"typeParameterList\":{typeParameterListBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteSubpatternSyntax(StringBuilder builder, SubpatternSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ExpressionColon != null)
        {
            var expressionColonBuilder = new StringBuilder();
            WriteSyntaxNode(expressionColonBuilder, syntaxNode.ExpressionColon);
            properties.Add($"\"expressionColon\":{expressionColonBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.NameColon != null)
        {
            var nameColonBuilder = new StringBuilder();
            WriteNameColonSyntax(nameColonBuilder, syntaxNode.NameColon);
            properties.Add($"\"nameColon\":{nameColonBuilder}");
        }
        if (syntaxNode.Pattern != null)
        {
            var patternBuilder = new StringBuilder();
            WriteSyntaxNode(patternBuilder, syntaxNode.Pattern);
            properties.Add($"\"pattern\":{patternBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteSwitchExpressionArmSyntax(StringBuilder builder, SwitchExpressionArmSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.EqualsGreaterThanToken != default)
        {
            var equalsGreaterThanTokenBuilder = new StringBuilder();
            WriteSyntaxToken(equalsGreaterThanTokenBuilder, syntaxNode.EqualsGreaterThanToken);
            properties.Add($"\"equalsGreaterThanToken\":{equalsGreaterThanTokenBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Pattern != null)
        {
            var patternBuilder = new StringBuilder();
            WriteSyntaxNode(patternBuilder, syntaxNode.Pattern);
            properties.Add($"\"pattern\":{patternBuilder}");
        }
        if (syntaxNode.WhenClause != null)
        {
            var whenClauseBuilder = new StringBuilder();
            WriteWhenClauseSyntax(whenClauseBuilder, syntaxNode.WhenClause);
            properties.Add($"\"whenClause\":{whenClauseBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteSwitchExpressionSyntax(StringBuilder builder, SwitchExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var arms = new List<string>();
        foreach (var node in syntaxNode.Arms)
        {
            var innerBuilder = new StringBuilder();
            WriteSwitchExpressionArmSyntax(innerBuilder, node);
            arms.Add(innerBuilder.ToString());
        }
        properties.Add($"\"arms\":[{string.Join(",", arms)}]");
        if (syntaxNode.CloseBraceToken != default)
        {
            var closeBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBraceTokenBuilder, syntaxNode.CloseBraceToken);
            properties.Add($"\"closeBraceToken\":{closeBraceTokenBuilder}");
        }
        if (syntaxNode.GoverningExpression != null)
        {
            var governingExpressionBuilder = new StringBuilder();
            WriteSyntaxNode(governingExpressionBuilder, syntaxNode.GoverningExpression);
            properties.Add($"\"governingExpression\":{governingExpressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBraceToken != default)
        {
            var openBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBraceTokenBuilder, syntaxNode.OpenBraceToken);
            properties.Add($"\"openBraceToken\":{openBraceTokenBuilder}");
        }
        if (syntaxNode.SwitchKeyword != default)
        {
            var switchKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(switchKeywordBuilder, syntaxNode.SwitchKeyword);
            properties.Add($"\"switchKeyword\":{switchKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteSwitchSectionSyntax(StringBuilder builder, SwitchSectionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var labels = new List<string>();
        foreach (var node in syntaxNode.Labels)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            labels.Add(innerBuilder.ToString());
        }
        properties.Add($"\"labels\":[{string.Join(",", labels)}]");
        var statements = new List<string>();
        foreach (var node in syntaxNode.Statements)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            statements.Add(innerBuilder.ToString());
        }
        properties.Add($"\"statements\":[{string.Join(",", statements)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteSwitchStatementSyntax(StringBuilder builder, SwitchStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.CloseBraceToken != default)
        {
            var closeBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeBraceTokenBuilder, syntaxNode.CloseBraceToken);
            properties.Add($"\"closeBraceToken\":{closeBraceTokenBuilder}");
        }
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenBraceToken != default)
        {
            var openBraceTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openBraceTokenBuilder, syntaxNode.OpenBraceToken);
            properties.Add($"\"openBraceToken\":{openBraceTokenBuilder}");
        }
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        var sections = new List<string>();
        foreach (var node in syntaxNode.Sections)
        {
            var innerBuilder = new StringBuilder();
            WriteSwitchSectionSyntax(innerBuilder, node);
            sections.Add(innerBuilder.ToString());
        }
        properties.Add($"\"sections\":[{string.Join(",", sections)}]");
        if (syntaxNode.SwitchKeyword != default)
        {
            var switchKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(switchKeywordBuilder, syntaxNode.SwitchKeyword);
            properties.Add($"\"switchKeyword\":{switchKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteThisExpressionSyntax(StringBuilder builder, ThisExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Token != default)
        {
            var tokenBuilder = new StringBuilder();
            WriteSyntaxToken(tokenBuilder, syntaxNode.Token);
            properties.Add($"\"token\":{tokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteThrowExpressionSyntax(StringBuilder builder, ThrowExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.ThrowKeyword != default)
        {
            var throwKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(throwKeywordBuilder, syntaxNode.ThrowKeyword);
            properties.Add($"\"throwKeyword\":{throwKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteThrowStatementSyntax(StringBuilder builder, ThrowStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.ThrowKeyword != default)
        {
            var throwKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(throwKeywordBuilder, syntaxNode.ThrowKeyword);
            properties.Add($"\"throwKeyword\":{throwKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteTryStatementSyntax(StringBuilder builder, TryStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Block != null)
        {
            var blockBuilder = new StringBuilder();
            WriteBlockSyntax(blockBuilder, syntaxNode.Block);
            properties.Add($"\"block\":{blockBuilder}");
        }
        var catches = new List<string>();
        foreach (var node in syntaxNode.Catches)
        {
            var innerBuilder = new StringBuilder();
            WriteCatchClauseSyntax(innerBuilder, node);
            catches.Add(innerBuilder.ToString());
        }
        properties.Add($"\"catches\":[{string.Join(",", catches)}]");
        if (syntaxNode.Finally != null)
        {
            var finallyBuilder = new StringBuilder();
            WriteFinallyClauseSyntax(finallyBuilder, syntaxNode.Finally);
            properties.Add($"\"finally\":{finallyBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.TryKeyword != default)
        {
            var tryKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(tryKeywordBuilder, syntaxNode.TryKeyword);
            properties.Add($"\"tryKeyword\":{tryKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteTupleElementSyntax(StringBuilder builder, TupleElementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteTupleExpressionSyntax(StringBuilder builder, TupleExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var arguments = new List<string>();
        foreach (var node in syntaxNode.Arguments)
        {
            var innerBuilder = new StringBuilder();
            WriteArgumentSyntax(innerBuilder, node);
            arguments.Add(innerBuilder.ToString());
        }
        properties.Add($"\"arguments\":[{string.Join(",", arguments)}]");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteTupleTypeSyntax(StringBuilder builder, TupleTypeSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        var elements = new List<string>();
        foreach (var node in syntaxNode.Elements)
        {
            var innerBuilder = new StringBuilder();
            WriteTupleElementSyntax(innerBuilder, node);
            elements.Add(innerBuilder.ToString());
        }
        properties.Add($"\"elements\":[{string.Join(",", elements)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        properties.Add(WriteBoolean("isNint", syntaxNode.IsNint));
        properties.Add(WriteBoolean("isNotNull", syntaxNode.IsNotNull));
        properties.Add(WriteBoolean("isNuint", syntaxNode.IsNuint));
        properties.Add(WriteBoolean("isUnmanaged", syntaxNode.IsUnmanaged));
        properties.Add(WriteBoolean("isVar", syntaxNode.IsVar));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteTypeArgumentListSyntax(StringBuilder builder, TypeArgumentListSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var arguments = new List<string>();
        foreach (var node in syntaxNode.Arguments)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            arguments.Add(innerBuilder.ToString());
        }
        properties.Add($"\"arguments\":[{string.Join(",", arguments)}]");
        if (syntaxNode.GreaterThanToken != default)
        {
            var greaterThanTokenBuilder = new StringBuilder();
            WriteSyntaxToken(greaterThanTokenBuilder, syntaxNode.GreaterThanToken);
            properties.Add($"\"greaterThanToken\":{greaterThanTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.LessThanToken != default)
        {
            var lessThanTokenBuilder = new StringBuilder();
            WriteSyntaxToken(lessThanTokenBuilder, syntaxNode.LessThanToken);
            properties.Add($"\"lessThanToken\":{lessThanTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteTypeConstraintSyntax(StringBuilder builder, TypeConstraintSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteTypeCrefSyntax(StringBuilder builder, TypeCrefSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteTypeOfExpressionSyntax(StringBuilder builder, TypeOfExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Keyword != default)
        {
            var keywordBuilder = new StringBuilder();
            WriteSyntaxToken(keywordBuilder, syntaxNode.Keyword);
            properties.Add($"\"keyword\":{keywordBuilder}");
        }
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteTypeParameterConstraintClauseSyntax(StringBuilder builder, TypeParameterConstraintClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ColonToken != default)
        {
            var colonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(colonTokenBuilder, syntaxNode.ColonToken);
            properties.Add($"\"colonToken\":{colonTokenBuilder}");
        }
        var constraints = new List<string>();
        foreach (var node in syntaxNode.Constraints)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            constraints.Add(innerBuilder.ToString());
        }
        properties.Add($"\"constraints\":[{string.Join(",", constraints)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteIdentifierNameSyntax(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        if (syntaxNode.WhereKeyword != default)
        {
            var whereKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(whereKeywordBuilder, syntaxNode.WhereKeyword);
            properties.Add($"\"whereKeyword\":{whereKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteTypeParameterListSyntax(StringBuilder builder, TypeParameterListSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.GreaterThanToken != default)
        {
            var greaterThanTokenBuilder = new StringBuilder();
            WriteSyntaxToken(greaterThanTokenBuilder, syntaxNode.GreaterThanToken);
            properties.Add($"\"greaterThanToken\":{greaterThanTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.LessThanToken != default)
        {
            var lessThanTokenBuilder = new StringBuilder();
            WriteSyntaxToken(lessThanTokenBuilder, syntaxNode.LessThanToken);
            properties.Add($"\"lessThanToken\":{lessThanTokenBuilder}");
        }
        var parameters = new List<string>();
        foreach (var node in syntaxNode.Parameters)
        {
            var innerBuilder = new StringBuilder();
            WriteTypeParameterSyntax(innerBuilder, node);
            parameters.Add(innerBuilder.ToString());
        }
        properties.Add($"\"parameters\":[{string.Join(",", parameters)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteTypeParameterSyntax(StringBuilder builder, TypeParameterSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.VarianceKeyword != default)
        {
            var varianceKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(varianceKeywordBuilder, syntaxNode.VarianceKeyword);
            properties.Add($"\"varianceKeyword\":{varianceKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteTypePatternSyntax(StringBuilder builder, TypePatternSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteUnaryPatternSyntax(StringBuilder builder, UnaryPatternSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OperatorToken != default)
        {
            var operatorTokenBuilder = new StringBuilder();
            WriteSyntaxToken(operatorTokenBuilder, syntaxNode.OperatorToken);
            properties.Add($"\"operatorToken\":{operatorTokenBuilder}");
        }
        if (syntaxNode.Pattern != null)
        {
            var patternBuilder = new StringBuilder();
            WriteSyntaxNode(patternBuilder, syntaxNode.Pattern);
            properties.Add($"\"pattern\":{patternBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteUndefDirectiveTriviaSyntax(StringBuilder builder, UndefDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != default)
        {
            var nameBuilder = new StringBuilder();
            WriteSyntaxToken(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        if (syntaxNode.UndefKeyword != default)
        {
            var undefKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(undefKeywordBuilder, syntaxNode.UndefKeyword);
            properties.Add($"\"undefKeyword\":{undefKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteUnsafeStatementSyntax(StringBuilder builder, UnsafeStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Block != null)
        {
            var blockBuilder = new StringBuilder();
            WriteBlockSyntax(blockBuilder, syntaxNode.Block);
            properties.Add($"\"block\":{blockBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.UnsafeKeyword != default)
        {
            var unsafeKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(unsafeKeywordBuilder, syntaxNode.UnsafeKeyword);
            properties.Add($"\"unsafeKeyword\":{unsafeKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteUsingDirectiveSyntax(StringBuilder builder, UsingDirectiveSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Alias != null)
        {
            var aliasBuilder = new StringBuilder();
            WriteNameEqualsSyntax(aliasBuilder, syntaxNode.Alias);
            properties.Add($"\"alias\":{aliasBuilder}");
        }
        if (syntaxNode.GlobalKeyword != default)
        {
            var globalKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(globalKeywordBuilder, syntaxNode.GlobalKeyword);
            properties.Add($"\"globalKeyword\":{globalKeywordBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteSyntaxNode(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        if (syntaxNode.NamespaceOrType != null)
        {
            var namespaceOrTypeBuilder = new StringBuilder();
            WriteSyntaxNode(namespaceOrTypeBuilder, syntaxNode.NamespaceOrType);
            properties.Add($"\"namespaceOrType\":{namespaceOrTypeBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.StaticKeyword != default)
        {
            var staticKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(staticKeywordBuilder, syntaxNode.StaticKeyword);
            properties.Add($"\"staticKeyword\":{staticKeywordBuilder}");
        }
        if (syntaxNode.UnsafeKeyword != default)
        {
            var unsafeKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(unsafeKeywordBuilder, syntaxNode.UnsafeKeyword);
            properties.Add($"\"unsafeKeyword\":{unsafeKeywordBuilder}");
        }
        if (syntaxNode.UsingKeyword != default)
        {
            var usingKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(usingKeywordBuilder, syntaxNode.UsingKeyword);
            properties.Add($"\"usingKeyword\":{usingKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteUsingStatementSyntax(StringBuilder builder, UsingStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.AwaitKeyword != default)
        {
            var awaitKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(awaitKeywordBuilder, syntaxNode.AwaitKeyword);
            properties.Add($"\"awaitKeyword\":{awaitKeywordBuilder}");
        }
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Declaration != null)
        {
            var declarationBuilder = new StringBuilder();
            WriteVariableDeclarationSyntax(declarationBuilder, syntaxNode.Declaration);
            properties.Add($"\"declaration\":{declarationBuilder}");
        }
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.Statement != null)
        {
            var statementBuilder = new StringBuilder();
            WriteSyntaxNode(statementBuilder, syntaxNode.Statement);
            properties.Add($"\"statement\":{statementBuilder}");
        }
        if (syntaxNode.UsingKeyword != default)
        {
            var usingKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(usingKeywordBuilder, syntaxNode.UsingKeyword);
            properties.Add($"\"usingKeyword\":{usingKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteVariableDeclarationSyntax(StringBuilder builder, VariableDeclarationSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Type != null)
        {
            var typeBuilder = new StringBuilder();
            WriteSyntaxNode(typeBuilder, syntaxNode.Type);
            properties.Add($"\"type\":{typeBuilder}");
        }
        var variables = new List<string>();
        foreach (var node in syntaxNode.Variables)
        {
            var innerBuilder = new StringBuilder();
            WriteVariableDeclaratorSyntax(innerBuilder, node);
            variables.Add(innerBuilder.ToString());
        }
        properties.Add($"\"variables\":[{string.Join(",", variables)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteVariableDeclaratorSyntax(StringBuilder builder, VariableDeclaratorSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ArgumentList != null)
        {
            var argumentListBuilder = new StringBuilder();
            WriteBracketedArgumentListSyntax(argumentListBuilder, syntaxNode.ArgumentList);
            properties.Add($"\"argumentList\":{argumentListBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != default)
        {
            var identifierBuilder = new StringBuilder();
            WriteSyntaxToken(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        if (syntaxNode.Initializer != null)
        {
            var initializerBuilder = new StringBuilder();
            WriteEqualsValueClauseSyntax(initializerBuilder, syntaxNode.Initializer);
            properties.Add($"\"initializer\":{initializerBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteVarPatternSyntax(StringBuilder builder, VarPatternSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Designation != null)
        {
            var designationBuilder = new StringBuilder();
            WriteSyntaxNode(designationBuilder, syntaxNode.Designation);
            properties.Add($"\"designation\":{designationBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.VarKeyword != default)
        {
            var varKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(varKeywordBuilder, syntaxNode.VarKeyword);
            properties.Add($"\"varKeyword\":{varKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteWarningDirectiveTriviaSyntax(StringBuilder builder, WarningDirectiveTriviaSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.DirectiveNameToken != default)
        {
            var directiveNameTokenBuilder = new StringBuilder();
            WriteSyntaxToken(directiveNameTokenBuilder, syntaxNode.DirectiveNameToken);
            properties.Add($"\"directiveNameToken\":{directiveNameTokenBuilder}");
        }
        if (syntaxNode.EndOfDirectiveToken != default)
        {
            var endOfDirectiveTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endOfDirectiveTokenBuilder, syntaxNode.EndOfDirectiveToken);
            properties.Add($"\"endOfDirectiveToken\":{endOfDirectiveTokenBuilder}");
        }
        if (syntaxNode.HashToken != default)
        {
            var hashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(hashTokenBuilder, syntaxNode.HashToken);
            properties.Add($"\"hashToken\":{hashTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isActive", syntaxNode.IsActive));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.WarningKeyword != default)
        {
            var warningKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(warningKeywordBuilder, syntaxNode.WarningKeyword);
            properties.Add($"\"warningKeyword\":{warningKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteWhenClauseSyntax(StringBuilder builder, WhenClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Condition != null)
        {
            var conditionBuilder = new StringBuilder();
            WriteSyntaxNode(conditionBuilder, syntaxNode.Condition);
            properties.Add($"\"condition\":{conditionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.WhenKeyword != default)
        {
            var whenKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(whenKeywordBuilder, syntaxNode.WhenKeyword);
            properties.Add($"\"whenKeyword\":{whenKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteWhereClauseSyntax(StringBuilder builder, WhereClauseSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Condition != null)
        {
            var conditionBuilder = new StringBuilder();
            WriteSyntaxNode(conditionBuilder, syntaxNode.Condition);
            properties.Add($"\"condition\":{conditionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.WhereKeyword != default)
        {
            var whereKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(whereKeywordBuilder, syntaxNode.WhereKeyword);
            properties.Add($"\"whereKeyword\":{whereKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteWhileStatementSyntax(StringBuilder builder, WhileStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.CloseParenToken != default)
        {
            var closeParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(closeParenTokenBuilder, syntaxNode.CloseParenToken);
            properties.Add($"\"closeParenToken\":{closeParenTokenBuilder}");
        }
        if (syntaxNode.Condition != null)
        {
            var conditionBuilder = new StringBuilder();
            WriteSyntaxNode(conditionBuilder, syntaxNode.Condition);
            properties.Add($"\"condition\":{conditionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.OpenParenToken != default)
        {
            var openParenTokenBuilder = new StringBuilder();
            WriteSyntaxToken(openParenTokenBuilder, syntaxNode.OpenParenToken);
            properties.Add($"\"openParenToken\":{openParenTokenBuilder}");
        }
        if (syntaxNode.Statement != null)
        {
            var statementBuilder = new StringBuilder();
            WriteSyntaxNode(statementBuilder, syntaxNode.Statement);
            properties.Add($"\"statement\":{statementBuilder}");
        }
        if (syntaxNode.WhileKeyword != default)
        {
            var whileKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(whileKeywordBuilder, syntaxNode.WhileKeyword);
            properties.Add($"\"whileKeyword\":{whileKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteWithExpressionSyntax(StringBuilder builder, WithExpressionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Initializer != null)
        {
            var initializerBuilder = new StringBuilder();
            WriteInitializerExpressionSyntax(initializerBuilder, syntaxNode.Initializer);
            properties.Add($"\"initializer\":{initializerBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.WithKeyword != default)
        {
            var withKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(withKeywordBuilder, syntaxNode.WithKeyword);
            properties.Add($"\"withKeyword\":{withKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteXmlCDataSectionSyntax(StringBuilder builder, XmlCDataSectionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.EndCDataToken != default)
        {
            var endCDataTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endCDataTokenBuilder, syntaxNode.EndCDataToken);
            properties.Add($"\"endCDataToken\":{endCDataTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.StartCDataToken != default)
        {
            var startCDataTokenBuilder = new StringBuilder();
            WriteSyntaxToken(startCDataTokenBuilder, syntaxNode.StartCDataToken);
            properties.Add($"\"startCDataToken\":{startCDataTokenBuilder}");
        }
        var textTokens = new List<string>();
        foreach (var node in syntaxNode.TextTokens)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            textTokens.Add(innerBuilder.ToString());
        }
        properties.Add($"\"textTokens\":[{string.Join(",", textTokens)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteXmlCommentSyntax(StringBuilder builder, XmlCommentSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.LessThanExclamationMinusMinusToken != default)
        {
            var lessThanExclamationMinusMinusTokenBuilder = new StringBuilder();
            WriteSyntaxToken(lessThanExclamationMinusMinusTokenBuilder, syntaxNode.LessThanExclamationMinusMinusToken);
            properties.Add($"\"lessThanExclamationMinusMinusToken\":{lessThanExclamationMinusMinusTokenBuilder}");
        }
        if (syntaxNode.MinusMinusGreaterThanToken != default)
        {
            var minusMinusGreaterThanTokenBuilder = new StringBuilder();
            WriteSyntaxToken(minusMinusGreaterThanTokenBuilder, syntaxNode.MinusMinusGreaterThanToken);
            properties.Add($"\"minusMinusGreaterThanToken\":{minusMinusGreaterThanTokenBuilder}");
        }
        var textTokens = new List<string>();
        foreach (var node in syntaxNode.TextTokens)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            textTokens.Add(innerBuilder.ToString());
        }
        properties.Add($"\"textTokens\":[{string.Join(",", textTokens)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteXmlCrefAttributeSyntax(StringBuilder builder, XmlCrefAttributeSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.Cref != null)
        {
            var crefBuilder = new StringBuilder();
            WriteSyntaxNode(crefBuilder, syntaxNode.Cref);
            properties.Add($"\"cref\":{crefBuilder}");
        }
        if (syntaxNode.EndQuoteToken != default)
        {
            var endQuoteTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endQuoteTokenBuilder, syntaxNode.EndQuoteToken);
            properties.Add($"\"endQuoteToken\":{endQuoteTokenBuilder}");
        }
        if (syntaxNode.EqualsToken != default)
        {
            var equalsTokenBuilder = new StringBuilder();
            WriteSyntaxToken(equalsTokenBuilder, syntaxNode.EqualsToken);
            properties.Add($"\"equalsToken\":{equalsTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteXmlNameSyntax(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        if (syntaxNode.StartQuoteToken != default)
        {
            var startQuoteTokenBuilder = new StringBuilder();
            WriteSyntaxToken(startQuoteTokenBuilder, syntaxNode.StartQuoteToken);
            properties.Add($"\"startQuoteToken\":{startQuoteTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteXmlElementEndTagSyntax(StringBuilder builder, XmlElementEndTagSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.GreaterThanToken != default)
        {
            var greaterThanTokenBuilder = new StringBuilder();
            WriteSyntaxToken(greaterThanTokenBuilder, syntaxNode.GreaterThanToken);
            properties.Add($"\"greaterThanToken\":{greaterThanTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.LessThanSlashToken != default)
        {
            var lessThanSlashTokenBuilder = new StringBuilder();
            WriteSyntaxToken(lessThanSlashTokenBuilder, syntaxNode.LessThanSlashToken);
            properties.Add($"\"lessThanSlashToken\":{lessThanSlashTokenBuilder}");
        }
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteXmlNameSyntax(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteXmlElementStartTagSyntax(StringBuilder builder, XmlElementStartTagSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributes = new List<string>();
        foreach (var node in syntaxNode.Attributes)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            attributes.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributes\":[{string.Join(",", attributes)}]");
        if (syntaxNode.GreaterThanToken != default)
        {
            var greaterThanTokenBuilder = new StringBuilder();
            WriteSyntaxToken(greaterThanTokenBuilder, syntaxNode.GreaterThanToken);
            properties.Add($"\"greaterThanToken\":{greaterThanTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.LessThanToken != default)
        {
            var lessThanTokenBuilder = new StringBuilder();
            WriteSyntaxToken(lessThanTokenBuilder, syntaxNode.LessThanToken);
            properties.Add($"\"lessThanToken\":{lessThanTokenBuilder}");
        }
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteXmlNameSyntax(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteXmlElementSyntax(StringBuilder builder, XmlElementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var content = new List<string>();
        foreach (var node in syntaxNode.Content)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            content.Add(innerBuilder.ToString());
        }
        properties.Add($"\"content\":[{string.Join(",", content)}]");
        if (syntaxNode.EndTag != null)
        {
            var endTagBuilder = new StringBuilder();
            WriteXmlElementEndTagSyntax(endTagBuilder, syntaxNode.EndTag);
            properties.Add($"\"endTag\":{endTagBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.StartTag != null)
        {
            var startTagBuilder = new StringBuilder();
            WriteXmlElementStartTagSyntax(startTagBuilder, syntaxNode.StartTag);
            properties.Add($"\"startTag\":{startTagBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteXmlEmptyElementSyntax(StringBuilder builder, XmlEmptyElementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributes = new List<string>();
        foreach (var node in syntaxNode.Attributes)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxNode(innerBuilder, node);
            attributes.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributes\":[{string.Join(",", attributes)}]");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.LessThanToken != default)
        {
            var lessThanTokenBuilder = new StringBuilder();
            WriteSyntaxToken(lessThanTokenBuilder, syntaxNode.LessThanToken);
            properties.Add($"\"lessThanToken\":{lessThanTokenBuilder}");
        }
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteXmlNameSyntax(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        if (syntaxNode.SlashGreaterThanToken != default)
        {
            var slashGreaterThanTokenBuilder = new StringBuilder();
            WriteSyntaxToken(slashGreaterThanTokenBuilder, syntaxNode.SlashGreaterThanToken);
            properties.Add($"\"slashGreaterThanToken\":{slashGreaterThanTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteXmlNameAttributeSyntax(StringBuilder builder, XmlNameAttributeSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.EndQuoteToken != default)
        {
            var endQuoteTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endQuoteTokenBuilder, syntaxNode.EndQuoteToken);
            properties.Add($"\"endQuoteToken\":{endQuoteTokenBuilder}");
        }
        if (syntaxNode.EqualsToken != default)
        {
            var equalsTokenBuilder = new StringBuilder();
            WriteSyntaxToken(equalsTokenBuilder, syntaxNode.EqualsToken);
            properties.Add($"\"equalsToken\":{equalsTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        if (syntaxNode.Identifier != null)
        {
            var identifierBuilder = new StringBuilder();
            WriteIdentifierNameSyntax(identifierBuilder, syntaxNode.Identifier);
            properties.Add($"\"identifier\":{identifierBuilder}");
        }
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteXmlNameSyntax(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        if (syntaxNode.StartQuoteToken != default)
        {
            var startQuoteTokenBuilder = new StringBuilder();
            WriteSyntaxToken(startQuoteTokenBuilder, syntaxNode.StartQuoteToken);
            properties.Add($"\"startQuoteToken\":{startQuoteTokenBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteXmlNameSyntax(StringBuilder builder, XmlNameSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.LocalName != default)
        {
            var localNameBuilder = new StringBuilder();
            WriteSyntaxToken(localNameBuilder, syntaxNode.LocalName);
            properties.Add($"\"localName\":{localNameBuilder}");
        }
        if (syntaxNode.Prefix != null)
        {
            var prefixBuilder = new StringBuilder();
            WriteXmlPrefixSyntax(prefixBuilder, syntaxNode.Prefix);
            properties.Add($"\"prefix\":{prefixBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteXmlPrefixSyntax(StringBuilder builder, XmlPrefixSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.ColonToken != default)
        {
            var colonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(colonTokenBuilder, syntaxNode.ColonToken);
            properties.Add($"\"colonToken\":{colonTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Prefix != default)
        {
            var prefixBuilder = new StringBuilder();
            WriteSyntaxToken(prefixBuilder, syntaxNode.Prefix);
            properties.Add($"\"prefix\":{prefixBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteXmlProcessingInstructionSyntax(StringBuilder builder, XmlProcessingInstructionSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.EndProcessingInstructionToken != default)
        {
            var endProcessingInstructionTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endProcessingInstructionTokenBuilder, syntaxNode.EndProcessingInstructionToken);
            properties.Add($"\"endProcessingInstructionToken\":{endProcessingInstructionTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteXmlNameSyntax(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        if (syntaxNode.StartProcessingInstructionToken != default)
        {
            var startProcessingInstructionTokenBuilder = new StringBuilder();
            WriteSyntaxToken(startProcessingInstructionTokenBuilder, syntaxNode.StartProcessingInstructionToken);
            properties.Add($"\"startProcessingInstructionToken\":{startProcessingInstructionTokenBuilder}");
        }
        var textTokens = new List<string>();
        foreach (var node in syntaxNode.TextTokens)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            textTokens.Add(innerBuilder.ToString());
        }
        properties.Add($"\"textTokens\":[{string.Join(",", textTokens)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteXmlTextAttributeSyntax(StringBuilder builder, XmlTextAttributeSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        if (syntaxNode.EndQuoteToken != default)
        {
            var endQuoteTokenBuilder = new StringBuilder();
            WriteSyntaxToken(endQuoteTokenBuilder, syntaxNode.EndQuoteToken);
            properties.Add($"\"endQuoteToken\":{endQuoteTokenBuilder}");
        }
        if (syntaxNode.EqualsToken != default)
        {
            var equalsTokenBuilder = new StringBuilder();
            WriteSyntaxToken(equalsTokenBuilder, syntaxNode.EqualsToken);
            properties.Add($"\"equalsToken\":{equalsTokenBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.Name != null)
        {
            var nameBuilder = new StringBuilder();
            WriteXmlNameSyntax(nameBuilder, syntaxNode.Name);
            properties.Add($"\"name\":{nameBuilder}");
        }
        if (syntaxNode.StartQuoteToken != default)
        {
            var startQuoteTokenBuilder = new StringBuilder();
            WriteSyntaxToken(startQuoteTokenBuilder, syntaxNode.StartQuoteToken);
            properties.Add($"\"startQuoteToken\":{startQuoteTokenBuilder}");
        }
        var textTokens = new List<string>();
        foreach (var node in syntaxNode.TextTokens)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            textTokens.Add(innerBuilder.ToString());
        }
        properties.Add($"\"textTokens\":[{string.Join(",", textTokens)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteXmlTextSyntax(StringBuilder builder, XmlTextSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        var textTokens = new List<string>();
        foreach (var node in syntaxNode.TextTokens)
        {
            var innerBuilder = new StringBuilder();
            WriteSyntaxToken(innerBuilder, node);
            textTokens.Add(innerBuilder.ToString());
        }
        properties.Add($"\"textTokens\":[{string.Join(",", textTokens)}]");
        builder.AppendJoin(",", properties).Append('}');
    }

    public static void WriteYieldStatementSyntax(StringBuilder builder, YieldStatementSyntax syntaxNode)
    {
        builder.Append('{');
        var properties = new List<string>();
        properties.Add($"\"nodeType\":\"{GetNodeType(syntaxNode.GetType())}\"");
        properties.Add($"\"kind\":\"{syntaxNode.Kind()}\"");
        var attributeLists = new List<string>();
        foreach (var node in syntaxNode.AttributeLists)
        {
            var innerBuilder = new StringBuilder();
            WriteAttributeListSyntax(innerBuilder, node);
            attributeLists.Add(innerBuilder.ToString());
        }
        properties.Add($"\"attributeLists\":[{string.Join(",", attributeLists)}]");
        if (syntaxNode.Expression != null)
        {
            var expressionBuilder = new StringBuilder();
            WriteSyntaxNode(expressionBuilder, syntaxNode.Expression);
            properties.Add($"\"expression\":{expressionBuilder}");
        }
        properties.Add(WriteBoolean("hasLeadingTrivia", syntaxNode.HasLeadingTrivia));
        properties.Add(WriteBoolean("hasTrailingTrivia", syntaxNode.HasTrailingTrivia));
        properties.Add(WriteBoolean("isMissing", syntaxNode.IsMissing));
        if (syntaxNode.ReturnOrBreakKeyword != default)
        {
            var returnOrBreakKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(returnOrBreakKeywordBuilder, syntaxNode.ReturnOrBreakKeyword);
            properties.Add($"\"returnOrBreakKeyword\":{returnOrBreakKeywordBuilder}");
        }
        if (syntaxNode.SemicolonToken != default)
        {
            var semicolonTokenBuilder = new StringBuilder();
            WriteSyntaxToken(semicolonTokenBuilder, syntaxNode.SemicolonToken);
            properties.Add($"\"semicolonToken\":{semicolonTokenBuilder}");
        }
        if (syntaxNode.YieldKeyword != default)
        {
            var yieldKeywordBuilder = new StringBuilder();
            WriteSyntaxToken(yieldKeywordBuilder, syntaxNode.YieldKeyword);
            properties.Add($"\"yieldKeyword\":{yieldKeywordBuilder}");
        }
        builder.AppendJoin(",", properties).Append('}');
    }
}