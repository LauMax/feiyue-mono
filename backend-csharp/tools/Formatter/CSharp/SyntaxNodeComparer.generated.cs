using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Feiyue.Formatter.CSharp;

internal sealed partial class SyntaxNodeComparer
{
    private CompareResult Compare((SyntaxNode originalNode, SyntaxNode originalParent) original, (SyntaxNode formattedNode, SyntaxNode formattedParent) formatted)
    {
        var (originalNode, originalParent) = original;
        var (formattedNode, formattedParent) = formatted;
        if (originalNode is null && formattedNode is null)
            return default;

        if (originalNode is null || formattedNode is null)
            return NotEqual(originalParent, formattedParent);

        var type = originalNode?.GetType();
        if (type != formattedNode?.GetType())
            return NotEqual(originalNode, formattedNode);

        if (originalNode.RawKind != formattedNode.RawKind)
            return NotEqual(originalNode, formattedNode);

        switch (originalNode)
        {
            case AccessorDeclarationSyntax accessorDeclarationSyntax:
                return CompareAccessorDeclarationSyntax(accessorDeclarationSyntax, formattedNode as AccessorDeclarationSyntax);
            case AccessorListSyntax accessorListSyntax:
                return CompareAccessorListSyntax(accessorListSyntax, formattedNode as AccessorListSyntax);
            case AliasQualifiedNameSyntax aliasQualifiedNameSyntax:
                return CompareAliasQualifiedNameSyntax(aliasQualifiedNameSyntax, formattedNode as AliasQualifiedNameSyntax);
            case AllowsConstraintClauseSyntax allowsConstraintClauseSyntax:
                return CompareAllowsConstraintClauseSyntax(allowsConstraintClauseSyntax, formattedNode as AllowsConstraintClauseSyntax);
            case AnonymousMethodExpressionSyntax anonymousMethodExpressionSyntax:
                return CompareAnonymousMethodExpressionSyntax(anonymousMethodExpressionSyntax, formattedNode as AnonymousMethodExpressionSyntax);
            case AnonymousObjectCreationExpressionSyntax anonymousObjectCreationExpressionSyntax:
                return CompareAnonymousObjectCreationExpressionSyntax(anonymousObjectCreationExpressionSyntax, formattedNode as AnonymousObjectCreationExpressionSyntax);
            case AnonymousObjectMemberDeclaratorSyntax anonymousObjectMemberDeclaratorSyntax:
                return CompareAnonymousObjectMemberDeclaratorSyntax(anonymousObjectMemberDeclaratorSyntax, formattedNode as AnonymousObjectMemberDeclaratorSyntax);
            case ArgumentListSyntax argumentListSyntax:
                return CompareArgumentListSyntax(argumentListSyntax, formattedNode as ArgumentListSyntax);
            case ArgumentSyntax argumentSyntax:
                return CompareArgumentSyntax(argumentSyntax, formattedNode as ArgumentSyntax);
            case ArrayCreationExpressionSyntax arrayCreationExpressionSyntax:
                return CompareArrayCreationExpressionSyntax(arrayCreationExpressionSyntax, formattedNode as ArrayCreationExpressionSyntax);
            case ArrayRankSpecifierSyntax arrayRankSpecifierSyntax:
                return CompareArrayRankSpecifierSyntax(arrayRankSpecifierSyntax, formattedNode as ArrayRankSpecifierSyntax);
            case ArrayTypeSyntax arrayTypeSyntax:
                return CompareArrayTypeSyntax(arrayTypeSyntax, formattedNode as ArrayTypeSyntax);
            case ArrowExpressionClauseSyntax arrowExpressionClauseSyntax:
                return CompareArrowExpressionClauseSyntax(arrowExpressionClauseSyntax, formattedNode as ArrowExpressionClauseSyntax);
            case AssignmentExpressionSyntax assignmentExpressionSyntax:
                return CompareAssignmentExpressionSyntax(assignmentExpressionSyntax, formattedNode as AssignmentExpressionSyntax);
            case AttributeArgumentListSyntax attributeArgumentListSyntax:
                return CompareAttributeArgumentListSyntax(attributeArgumentListSyntax, formattedNode as AttributeArgumentListSyntax);
            case AttributeArgumentSyntax attributeArgumentSyntax:
                return CompareAttributeArgumentSyntax(attributeArgumentSyntax, formattedNode as AttributeArgumentSyntax);
            case AttributeListSyntax attributeListSyntax:
                return CompareAttributeListSyntax(attributeListSyntax, formattedNode as AttributeListSyntax);
            case AttributeSyntax attributeSyntax:
                return CompareAttributeSyntax(attributeSyntax, formattedNode as AttributeSyntax);
            case AttributeTargetSpecifierSyntax attributeTargetSpecifierSyntax:
                return CompareAttributeTargetSpecifierSyntax(attributeTargetSpecifierSyntax, formattedNode as AttributeTargetSpecifierSyntax);
            case AwaitExpressionSyntax awaitExpressionSyntax:
                return CompareAwaitExpressionSyntax(awaitExpressionSyntax, formattedNode as AwaitExpressionSyntax);
            case BadDirectiveTriviaSyntax badDirectiveTriviaSyntax:
                return CompareBadDirectiveTriviaSyntax(badDirectiveTriviaSyntax, formattedNode as BadDirectiveTriviaSyntax);
            case BaseExpressionSyntax baseExpressionSyntax:
                return CompareBaseExpressionSyntax(baseExpressionSyntax, formattedNode as BaseExpressionSyntax);
            case BaseListSyntax baseListSyntax:
                return CompareBaseListSyntax(baseListSyntax, formattedNode as BaseListSyntax);
            case BinaryExpressionSyntax binaryExpressionSyntax:
                return CompareBinaryExpressionSyntax(binaryExpressionSyntax, formattedNode as BinaryExpressionSyntax);
            case BinaryPatternSyntax binaryPatternSyntax:
                return CompareBinaryPatternSyntax(binaryPatternSyntax, formattedNode as BinaryPatternSyntax);
            case BlockSyntax blockSyntax:
                return CompareBlockSyntax(blockSyntax, formattedNode as BlockSyntax);
            case BracketedArgumentListSyntax bracketedArgumentListSyntax:
                return CompareBracketedArgumentListSyntax(bracketedArgumentListSyntax, formattedNode as BracketedArgumentListSyntax);
            case BracketedParameterListSyntax bracketedParameterListSyntax:
                return CompareBracketedParameterListSyntax(bracketedParameterListSyntax, formattedNode as BracketedParameterListSyntax);
            case BreakStatementSyntax breakStatementSyntax:
                return CompareBreakStatementSyntax(breakStatementSyntax, formattedNode as BreakStatementSyntax);
            case CasePatternSwitchLabelSyntax casePatternSwitchLabelSyntax:
                return CompareCasePatternSwitchLabelSyntax(casePatternSwitchLabelSyntax, formattedNode as CasePatternSwitchLabelSyntax);
            case CaseSwitchLabelSyntax caseSwitchLabelSyntax:
                return CompareCaseSwitchLabelSyntax(caseSwitchLabelSyntax, formattedNode as CaseSwitchLabelSyntax);
            case CastExpressionSyntax castExpressionSyntax:
                return CompareCastExpressionSyntax(castExpressionSyntax, formattedNode as CastExpressionSyntax);
            case CatchClauseSyntax catchClauseSyntax:
                return CompareCatchClauseSyntax(catchClauseSyntax, formattedNode as CatchClauseSyntax);
            case CatchDeclarationSyntax catchDeclarationSyntax:
                return CompareCatchDeclarationSyntax(catchDeclarationSyntax, formattedNode as CatchDeclarationSyntax);
            case CatchFilterClauseSyntax catchFilterClauseSyntax:
                return CompareCatchFilterClauseSyntax(catchFilterClauseSyntax, formattedNode as CatchFilterClauseSyntax);
            case CheckedExpressionSyntax checkedExpressionSyntax:
                return CompareCheckedExpressionSyntax(checkedExpressionSyntax, formattedNode as CheckedExpressionSyntax);
            case CheckedStatementSyntax checkedStatementSyntax:
                return CompareCheckedStatementSyntax(checkedStatementSyntax, formattedNode as CheckedStatementSyntax);
            case ClassDeclarationSyntax classDeclarationSyntax:
                return CompareClassDeclarationSyntax(classDeclarationSyntax, formattedNode as ClassDeclarationSyntax);
            case ClassOrStructConstraintSyntax classOrStructConstraintSyntax:
                return CompareClassOrStructConstraintSyntax(classOrStructConstraintSyntax, formattedNode as ClassOrStructConstraintSyntax);
            case CollectionExpressionSyntax collectionExpressionSyntax:
                return CompareCollectionExpressionSyntax(collectionExpressionSyntax, formattedNode as CollectionExpressionSyntax);
            case CompilationUnitSyntax compilationUnitSyntax:
                return CompareCompilationUnitSyntax(compilationUnitSyntax, formattedNode as CompilationUnitSyntax);
            case ConditionalAccessExpressionSyntax conditionalAccessExpressionSyntax:
                return CompareConditionalAccessExpressionSyntax(conditionalAccessExpressionSyntax, formattedNode as ConditionalAccessExpressionSyntax);
            case ConditionalExpressionSyntax conditionalExpressionSyntax:
                return CompareConditionalExpressionSyntax(conditionalExpressionSyntax, formattedNode as ConditionalExpressionSyntax);
            case ConstantPatternSyntax constantPatternSyntax:
                return CompareConstantPatternSyntax(constantPatternSyntax, formattedNode as ConstantPatternSyntax);
            case ConstructorConstraintSyntax constructorConstraintSyntax:
                return CompareConstructorConstraintSyntax(constructorConstraintSyntax, formattedNode as ConstructorConstraintSyntax);
            case ConstructorDeclarationSyntax constructorDeclarationSyntax:
                return CompareConstructorDeclarationSyntax(constructorDeclarationSyntax, formattedNode as ConstructorDeclarationSyntax);
            case ConstructorInitializerSyntax constructorInitializerSyntax:
                return CompareConstructorInitializerSyntax(constructorInitializerSyntax, formattedNode as ConstructorInitializerSyntax);
            case ContinueStatementSyntax continueStatementSyntax:
                return CompareContinueStatementSyntax(continueStatementSyntax, formattedNode as ContinueStatementSyntax);
            case ConversionOperatorDeclarationSyntax conversionOperatorDeclarationSyntax:
                return CompareConversionOperatorDeclarationSyntax(conversionOperatorDeclarationSyntax, formattedNode as ConversionOperatorDeclarationSyntax);
            case ConversionOperatorMemberCrefSyntax conversionOperatorMemberCrefSyntax:
                return CompareConversionOperatorMemberCrefSyntax(conversionOperatorMemberCrefSyntax, formattedNode as ConversionOperatorMemberCrefSyntax);
            case CrefBracketedParameterListSyntax crefBracketedParameterListSyntax:
                return CompareCrefBracketedParameterListSyntax(crefBracketedParameterListSyntax, formattedNode as CrefBracketedParameterListSyntax);
            case CrefParameterListSyntax crefParameterListSyntax:
                return CompareCrefParameterListSyntax(crefParameterListSyntax, formattedNode as CrefParameterListSyntax);
            case CrefParameterSyntax crefParameterSyntax:
                return CompareCrefParameterSyntax(crefParameterSyntax, formattedNode as CrefParameterSyntax);
            case DeclarationExpressionSyntax declarationExpressionSyntax:
                return CompareDeclarationExpressionSyntax(declarationExpressionSyntax, formattedNode as DeclarationExpressionSyntax);
            case DeclarationPatternSyntax declarationPatternSyntax:
                return CompareDeclarationPatternSyntax(declarationPatternSyntax, formattedNode as DeclarationPatternSyntax);
            case DefaultConstraintSyntax defaultConstraintSyntax:
                return CompareDefaultConstraintSyntax(defaultConstraintSyntax, formattedNode as DefaultConstraintSyntax);
            case DefaultExpressionSyntax defaultExpressionSyntax:
                return CompareDefaultExpressionSyntax(defaultExpressionSyntax, formattedNode as DefaultExpressionSyntax);
            case DefaultSwitchLabelSyntax defaultSwitchLabelSyntax:
                return CompareDefaultSwitchLabelSyntax(defaultSwitchLabelSyntax, formattedNode as DefaultSwitchLabelSyntax);
            case DefineDirectiveTriviaSyntax defineDirectiveTriviaSyntax:
                return CompareDefineDirectiveTriviaSyntax(defineDirectiveTriviaSyntax, formattedNode as DefineDirectiveTriviaSyntax);
            case DelegateDeclarationSyntax delegateDeclarationSyntax:
                return CompareDelegateDeclarationSyntax(delegateDeclarationSyntax, formattedNode as DelegateDeclarationSyntax);
            case DestructorDeclarationSyntax destructorDeclarationSyntax:
                return CompareDestructorDeclarationSyntax(destructorDeclarationSyntax, formattedNode as DestructorDeclarationSyntax);
            case DiscardDesignationSyntax discardDesignationSyntax:
                return CompareDiscardDesignationSyntax(discardDesignationSyntax, formattedNode as DiscardDesignationSyntax);
            case DiscardPatternSyntax discardPatternSyntax:
                return CompareDiscardPatternSyntax(discardPatternSyntax, formattedNode as DiscardPatternSyntax);
            case DocumentationCommentTriviaSyntax documentationCommentTriviaSyntax:
                return CompareDocumentationCommentTriviaSyntax(documentationCommentTriviaSyntax, formattedNode as DocumentationCommentTriviaSyntax);
            case DoStatementSyntax doStatementSyntax:
                return CompareDoStatementSyntax(doStatementSyntax, formattedNode as DoStatementSyntax);
            case ElementAccessExpressionSyntax elementAccessExpressionSyntax:
                return CompareElementAccessExpressionSyntax(elementAccessExpressionSyntax, formattedNode as ElementAccessExpressionSyntax);
            case ElementBindingExpressionSyntax elementBindingExpressionSyntax:
                return CompareElementBindingExpressionSyntax(elementBindingExpressionSyntax, formattedNode as ElementBindingExpressionSyntax);
            case ElifDirectiveTriviaSyntax elifDirectiveTriviaSyntax:
                return CompareElifDirectiveTriviaSyntax(elifDirectiveTriviaSyntax, formattedNode as ElifDirectiveTriviaSyntax);
            case ElseClauseSyntax elseClauseSyntax:
                return CompareElseClauseSyntax(elseClauseSyntax, formattedNode as ElseClauseSyntax);
            case ElseDirectiveTriviaSyntax elseDirectiveTriviaSyntax:
                return CompareElseDirectiveTriviaSyntax(elseDirectiveTriviaSyntax, formattedNode as ElseDirectiveTriviaSyntax);
            case EmptyStatementSyntax emptyStatementSyntax:
                return CompareEmptyStatementSyntax(emptyStatementSyntax, formattedNode as EmptyStatementSyntax);
            case EndIfDirectiveTriviaSyntax endIfDirectiveTriviaSyntax:
                return CompareEndIfDirectiveTriviaSyntax(endIfDirectiveTriviaSyntax, formattedNode as EndIfDirectiveTriviaSyntax);
            case EndRegionDirectiveTriviaSyntax endRegionDirectiveTriviaSyntax:
                return CompareEndRegionDirectiveTriviaSyntax(endRegionDirectiveTriviaSyntax, formattedNode as EndRegionDirectiveTriviaSyntax);
            case EnumDeclarationSyntax enumDeclarationSyntax:
                return CompareEnumDeclarationSyntax(enumDeclarationSyntax, formattedNode as EnumDeclarationSyntax);
            case EnumMemberDeclarationSyntax enumMemberDeclarationSyntax:
                return CompareEnumMemberDeclarationSyntax(enumMemberDeclarationSyntax, formattedNode as EnumMemberDeclarationSyntax);
            case EqualsValueClauseSyntax equalsValueClauseSyntax:
                return CompareEqualsValueClauseSyntax(equalsValueClauseSyntax, formattedNode as EqualsValueClauseSyntax);
            case ErrorDirectiveTriviaSyntax errorDirectiveTriviaSyntax:
                return CompareErrorDirectiveTriviaSyntax(errorDirectiveTriviaSyntax, formattedNode as ErrorDirectiveTriviaSyntax);
            case EventDeclarationSyntax eventDeclarationSyntax:
                return CompareEventDeclarationSyntax(eventDeclarationSyntax, formattedNode as EventDeclarationSyntax);
            case EventFieldDeclarationSyntax eventFieldDeclarationSyntax:
                return CompareEventFieldDeclarationSyntax(eventFieldDeclarationSyntax, formattedNode as EventFieldDeclarationSyntax);
            case ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax:
                return CompareExplicitInterfaceSpecifierSyntax(explicitInterfaceSpecifierSyntax, formattedNode as ExplicitInterfaceSpecifierSyntax);
            case ExpressionColonSyntax expressionColonSyntax:
                return CompareExpressionColonSyntax(expressionColonSyntax, formattedNode as ExpressionColonSyntax);
            case ExpressionElementSyntax expressionElementSyntax:
                return CompareExpressionElementSyntax(expressionElementSyntax, formattedNode as ExpressionElementSyntax);
            case ExpressionStatementSyntax expressionStatementSyntax:
                return CompareExpressionStatementSyntax(expressionStatementSyntax, formattedNode as ExpressionStatementSyntax);
            case ExternAliasDirectiveSyntax externAliasDirectiveSyntax:
                return CompareExternAliasDirectiveSyntax(externAliasDirectiveSyntax, formattedNode as ExternAliasDirectiveSyntax);
            case FieldDeclarationSyntax fieldDeclarationSyntax:
                return CompareFieldDeclarationSyntax(fieldDeclarationSyntax, formattedNode as FieldDeclarationSyntax);
            case FieldExpressionSyntax fieldExpressionSyntax:
                return CompareFieldExpressionSyntax(fieldExpressionSyntax, formattedNode as FieldExpressionSyntax);
            case FileScopedNamespaceDeclarationSyntax fileScopedNamespaceDeclarationSyntax:
                return CompareFileScopedNamespaceDeclarationSyntax(fileScopedNamespaceDeclarationSyntax, formattedNode as FileScopedNamespaceDeclarationSyntax);
            case FinallyClauseSyntax finallyClauseSyntax:
                return CompareFinallyClauseSyntax(finallyClauseSyntax, formattedNode as FinallyClauseSyntax);
            case FixedStatementSyntax fixedStatementSyntax:
                return CompareFixedStatementSyntax(fixedStatementSyntax, formattedNode as FixedStatementSyntax);
            case ForEachStatementSyntax forEachStatementSyntax:
                return CompareForEachStatementSyntax(forEachStatementSyntax, formattedNode as ForEachStatementSyntax);
            case ForEachVariableStatementSyntax forEachVariableStatementSyntax:
                return CompareForEachVariableStatementSyntax(forEachVariableStatementSyntax, formattedNode as ForEachVariableStatementSyntax);
            case ForStatementSyntax forStatementSyntax:
                return CompareForStatementSyntax(forStatementSyntax, formattedNode as ForStatementSyntax);
            case FromClauseSyntax fromClauseSyntax:
                return CompareFromClauseSyntax(fromClauseSyntax, formattedNode as FromClauseSyntax);
            case FunctionPointerCallingConventionSyntax functionPointerCallingConventionSyntax:
                return CompareFunctionPointerCallingConventionSyntax(functionPointerCallingConventionSyntax, formattedNode as FunctionPointerCallingConventionSyntax);
            case FunctionPointerParameterListSyntax functionPointerParameterListSyntax:
                return CompareFunctionPointerParameterListSyntax(functionPointerParameterListSyntax, formattedNode as FunctionPointerParameterListSyntax);
            case FunctionPointerParameterSyntax functionPointerParameterSyntax:
                return CompareFunctionPointerParameterSyntax(functionPointerParameterSyntax, formattedNode as FunctionPointerParameterSyntax);
            case FunctionPointerTypeSyntax functionPointerTypeSyntax:
                return CompareFunctionPointerTypeSyntax(functionPointerTypeSyntax, formattedNode as FunctionPointerTypeSyntax);
            case FunctionPointerUnmanagedCallingConventionListSyntax functionPointerUnmanagedCallingConventionListSyntax:
                return CompareFunctionPointerUnmanagedCallingConventionListSyntax(
                    functionPointerUnmanagedCallingConventionListSyntax,
                    formattedNode as FunctionPointerUnmanagedCallingConventionListSyntax);
            case FunctionPointerUnmanagedCallingConventionSyntax functionPointerUnmanagedCallingConventionSyntax:
                return CompareFunctionPointerUnmanagedCallingConventionSyntax(
                    functionPointerUnmanagedCallingConventionSyntax,
                    formattedNode as FunctionPointerUnmanagedCallingConventionSyntax);
            case GenericNameSyntax genericNameSyntax:
                return CompareGenericNameSyntax(genericNameSyntax, formattedNode as GenericNameSyntax);
            case GlobalStatementSyntax globalStatementSyntax:
                return CompareGlobalStatementSyntax(globalStatementSyntax, formattedNode as GlobalStatementSyntax);
            case GotoStatementSyntax gotoStatementSyntax:
                return CompareGotoStatementSyntax(gotoStatementSyntax, formattedNode as GotoStatementSyntax);
            case GroupClauseSyntax groupClauseSyntax:
                return CompareGroupClauseSyntax(groupClauseSyntax, formattedNode as GroupClauseSyntax);
            case IdentifierNameSyntax identifierNameSyntax:
                return CompareIdentifierNameSyntax(identifierNameSyntax, formattedNode as IdentifierNameSyntax);
            case IfDirectiveTriviaSyntax ifDirectiveTriviaSyntax:
                return CompareIfDirectiveTriviaSyntax(ifDirectiveTriviaSyntax, formattedNode as IfDirectiveTriviaSyntax);
            case IfStatementSyntax ifStatementSyntax:
                return CompareIfStatementSyntax(ifStatementSyntax, formattedNode as IfStatementSyntax);
            case IgnoredDirectiveTriviaSyntax ignoredDirectiveTriviaSyntax:
                return CompareIgnoredDirectiveTriviaSyntax(ignoredDirectiveTriviaSyntax, formattedNode as IgnoredDirectiveTriviaSyntax);
            case ImplicitArrayCreationExpressionSyntax implicitArrayCreationExpressionSyntax:
                return CompareImplicitArrayCreationExpressionSyntax(implicitArrayCreationExpressionSyntax, formattedNode as ImplicitArrayCreationExpressionSyntax);
            case ImplicitElementAccessSyntax implicitElementAccessSyntax:
                return CompareImplicitElementAccessSyntax(implicitElementAccessSyntax, formattedNode as ImplicitElementAccessSyntax);
            case ImplicitObjectCreationExpressionSyntax implicitObjectCreationExpressionSyntax:
                return CompareImplicitObjectCreationExpressionSyntax(implicitObjectCreationExpressionSyntax, formattedNode as ImplicitObjectCreationExpressionSyntax);
            case ImplicitStackAllocArrayCreationExpressionSyntax implicitStackAllocArrayCreationExpressionSyntax:
                return CompareImplicitStackAllocArrayCreationExpressionSyntax(
                    implicitStackAllocArrayCreationExpressionSyntax,
                    formattedNode as ImplicitStackAllocArrayCreationExpressionSyntax);
            case IncompleteMemberSyntax incompleteMemberSyntax:
                return CompareIncompleteMemberSyntax(incompleteMemberSyntax, formattedNode as IncompleteMemberSyntax);
            case IndexerDeclarationSyntax indexerDeclarationSyntax:
                return CompareIndexerDeclarationSyntax(indexerDeclarationSyntax, formattedNode as IndexerDeclarationSyntax);
            case IndexerMemberCrefSyntax indexerMemberCrefSyntax:
                return CompareIndexerMemberCrefSyntax(indexerMemberCrefSyntax, formattedNode as IndexerMemberCrefSyntax);
            case InitializerExpressionSyntax initializerExpressionSyntax:
                return CompareInitializerExpressionSyntax(initializerExpressionSyntax, formattedNode as InitializerExpressionSyntax);
            case InterfaceDeclarationSyntax interfaceDeclarationSyntax:
                return CompareInterfaceDeclarationSyntax(interfaceDeclarationSyntax, formattedNode as InterfaceDeclarationSyntax);
            case InterpolatedStringExpressionSyntax interpolatedStringExpressionSyntax:
                return CompareInterpolatedStringExpressionSyntax(interpolatedStringExpressionSyntax, formattedNode as InterpolatedStringExpressionSyntax);
            case InterpolatedStringTextSyntax interpolatedStringTextSyntax:
                return CompareInterpolatedStringTextSyntax(interpolatedStringTextSyntax, formattedNode as InterpolatedStringTextSyntax);
            case InterpolationAlignmentClauseSyntax interpolationAlignmentClauseSyntax:
                return CompareInterpolationAlignmentClauseSyntax(interpolationAlignmentClauseSyntax, formattedNode as InterpolationAlignmentClauseSyntax);
            case InterpolationFormatClauseSyntax interpolationFormatClauseSyntax:
                return CompareInterpolationFormatClauseSyntax(interpolationFormatClauseSyntax, formattedNode as InterpolationFormatClauseSyntax);
            case InterpolationSyntax interpolationSyntax:
                return CompareInterpolationSyntax(interpolationSyntax, formattedNode as InterpolationSyntax);
            case InvocationExpressionSyntax invocationExpressionSyntax:
                return CompareInvocationExpressionSyntax(invocationExpressionSyntax, formattedNode as InvocationExpressionSyntax);
            case IsPatternExpressionSyntax isPatternExpressionSyntax:
                return CompareIsPatternExpressionSyntax(isPatternExpressionSyntax, formattedNode as IsPatternExpressionSyntax);
            case JoinClauseSyntax joinClauseSyntax:
                return CompareJoinClauseSyntax(joinClauseSyntax, formattedNode as JoinClauseSyntax);
            case JoinIntoClauseSyntax joinIntoClauseSyntax:
                return CompareJoinIntoClauseSyntax(joinIntoClauseSyntax, formattedNode as JoinIntoClauseSyntax);
            case LabeledStatementSyntax labeledStatementSyntax:
                return CompareLabeledStatementSyntax(labeledStatementSyntax, formattedNode as LabeledStatementSyntax);
            case LetClauseSyntax letClauseSyntax:
                return CompareLetClauseSyntax(letClauseSyntax, formattedNode as LetClauseSyntax);
            case LineDirectivePositionSyntax lineDirectivePositionSyntax:
                return CompareLineDirectivePositionSyntax(lineDirectivePositionSyntax, formattedNode as LineDirectivePositionSyntax);
            case LineDirectiveTriviaSyntax lineDirectiveTriviaSyntax:
                return CompareLineDirectiveTriviaSyntax(lineDirectiveTriviaSyntax, formattedNode as LineDirectiveTriviaSyntax);
            case LineSpanDirectiveTriviaSyntax lineSpanDirectiveTriviaSyntax:
                return CompareLineSpanDirectiveTriviaSyntax(lineSpanDirectiveTriviaSyntax, formattedNode as LineSpanDirectiveTriviaSyntax);
            case ListPatternSyntax listPatternSyntax:
                return CompareListPatternSyntax(listPatternSyntax, formattedNode as ListPatternSyntax);
            case LiteralExpressionSyntax literalExpressionSyntax:
                return CompareLiteralExpressionSyntax(literalExpressionSyntax, formattedNode as LiteralExpressionSyntax);
            case LoadDirectiveTriviaSyntax loadDirectiveTriviaSyntax:
                return CompareLoadDirectiveTriviaSyntax(loadDirectiveTriviaSyntax, formattedNode as LoadDirectiveTriviaSyntax);
            case LocalDeclarationStatementSyntax localDeclarationStatementSyntax:
                return CompareLocalDeclarationStatementSyntax(localDeclarationStatementSyntax, formattedNode as LocalDeclarationStatementSyntax);
            case LocalFunctionStatementSyntax localFunctionStatementSyntax:
                return CompareLocalFunctionStatementSyntax(localFunctionStatementSyntax, formattedNode as LocalFunctionStatementSyntax);
            case LockStatementSyntax lockStatementSyntax:
                return CompareLockStatementSyntax(lockStatementSyntax, formattedNode as LockStatementSyntax);
            case MakeRefExpressionSyntax makeRefExpressionSyntax:
                return CompareMakeRefExpressionSyntax(makeRefExpressionSyntax, formattedNode as MakeRefExpressionSyntax);
            case MemberAccessExpressionSyntax memberAccessExpressionSyntax:
                return CompareMemberAccessExpressionSyntax(memberAccessExpressionSyntax, formattedNode as MemberAccessExpressionSyntax);
            case MemberBindingExpressionSyntax memberBindingExpressionSyntax:
                return CompareMemberBindingExpressionSyntax(memberBindingExpressionSyntax, formattedNode as MemberBindingExpressionSyntax);
            case MethodDeclarationSyntax methodDeclarationSyntax:
                return CompareMethodDeclarationSyntax(methodDeclarationSyntax, formattedNode as MethodDeclarationSyntax);
            case NameColonSyntax nameColonSyntax:
                return CompareNameColonSyntax(nameColonSyntax, formattedNode as NameColonSyntax);
            case NameEqualsSyntax nameEqualsSyntax:
                return CompareNameEqualsSyntax(nameEqualsSyntax, formattedNode as NameEqualsSyntax);
            case NameMemberCrefSyntax nameMemberCrefSyntax:
                return CompareNameMemberCrefSyntax(nameMemberCrefSyntax, formattedNode as NameMemberCrefSyntax);
            case NamespaceDeclarationSyntax namespaceDeclarationSyntax:
                return CompareNamespaceDeclarationSyntax(namespaceDeclarationSyntax, formattedNode as NamespaceDeclarationSyntax);
            case NullableDirectiveTriviaSyntax nullableDirectiveTriviaSyntax:
                return CompareNullableDirectiveTriviaSyntax(nullableDirectiveTriviaSyntax, formattedNode as NullableDirectiveTriviaSyntax);
            case NullableTypeSyntax nullableTypeSyntax:
                return CompareNullableTypeSyntax(nullableTypeSyntax, formattedNode as NullableTypeSyntax);
            case ObjectCreationExpressionSyntax objectCreationExpressionSyntax:
                return CompareObjectCreationExpressionSyntax(objectCreationExpressionSyntax, formattedNode as ObjectCreationExpressionSyntax);
            case OmittedArraySizeExpressionSyntax omittedArraySizeExpressionSyntax:
                return CompareOmittedArraySizeExpressionSyntax(omittedArraySizeExpressionSyntax, formattedNode as OmittedArraySizeExpressionSyntax);
            case OmittedTypeArgumentSyntax omittedTypeArgumentSyntax:
                return CompareOmittedTypeArgumentSyntax(omittedTypeArgumentSyntax, formattedNode as OmittedTypeArgumentSyntax);
            case OperatorDeclarationSyntax operatorDeclarationSyntax:
                return CompareOperatorDeclarationSyntax(operatorDeclarationSyntax, formattedNode as OperatorDeclarationSyntax);
            case OperatorMemberCrefSyntax operatorMemberCrefSyntax:
                return CompareOperatorMemberCrefSyntax(operatorMemberCrefSyntax, formattedNode as OperatorMemberCrefSyntax);
            case OrderByClauseSyntax orderByClauseSyntax:
                return CompareOrderByClauseSyntax(orderByClauseSyntax, formattedNode as OrderByClauseSyntax);
            case OrderingSyntax orderingSyntax:
                return CompareOrderingSyntax(orderingSyntax, formattedNode as OrderingSyntax);
            case ParameterListSyntax parameterListSyntax:
                return CompareParameterListSyntax(parameterListSyntax, formattedNode as ParameterListSyntax);
            case ParameterSyntax parameterSyntax:
                return CompareParameterSyntax(parameterSyntax, formattedNode as ParameterSyntax);
            case ParenthesizedExpressionSyntax parenthesizedExpressionSyntax:
                return CompareParenthesizedExpressionSyntax(parenthesizedExpressionSyntax, formattedNode as ParenthesizedExpressionSyntax);
            case ParenthesizedLambdaExpressionSyntax parenthesizedLambdaExpressionSyntax:
                return CompareParenthesizedLambdaExpressionSyntax(parenthesizedLambdaExpressionSyntax, formattedNode as ParenthesizedLambdaExpressionSyntax);
            case ParenthesizedPatternSyntax parenthesizedPatternSyntax:
                return CompareParenthesizedPatternSyntax(parenthesizedPatternSyntax, formattedNode as ParenthesizedPatternSyntax);
            case ParenthesizedVariableDesignationSyntax parenthesizedVariableDesignationSyntax:
                return CompareParenthesizedVariableDesignationSyntax(parenthesizedVariableDesignationSyntax, formattedNode as ParenthesizedVariableDesignationSyntax);
            case PointerTypeSyntax pointerTypeSyntax:
                return ComparePointerTypeSyntax(pointerTypeSyntax, formattedNode as PointerTypeSyntax);
            case PositionalPatternClauseSyntax positionalPatternClauseSyntax:
                return ComparePositionalPatternClauseSyntax(positionalPatternClauseSyntax, formattedNode as PositionalPatternClauseSyntax);
            case PostfixUnaryExpressionSyntax postfixUnaryExpressionSyntax:
                return ComparePostfixUnaryExpressionSyntax(postfixUnaryExpressionSyntax, formattedNode as PostfixUnaryExpressionSyntax);
            case PragmaChecksumDirectiveTriviaSyntax pragmaChecksumDirectiveTriviaSyntax:
                return ComparePragmaChecksumDirectiveTriviaSyntax(pragmaChecksumDirectiveTriviaSyntax, formattedNode as PragmaChecksumDirectiveTriviaSyntax);
            case PragmaWarningDirectiveTriviaSyntax pragmaWarningDirectiveTriviaSyntax:
                return ComparePragmaWarningDirectiveTriviaSyntax(pragmaWarningDirectiveTriviaSyntax, formattedNode as PragmaWarningDirectiveTriviaSyntax);
            case PredefinedTypeSyntax predefinedTypeSyntax:
                return ComparePredefinedTypeSyntax(predefinedTypeSyntax, formattedNode as PredefinedTypeSyntax);
            case PrefixUnaryExpressionSyntax prefixUnaryExpressionSyntax:
                return ComparePrefixUnaryExpressionSyntax(prefixUnaryExpressionSyntax, formattedNode as PrefixUnaryExpressionSyntax);
            case PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeSyntax:
                return ComparePrimaryConstructorBaseTypeSyntax(primaryConstructorBaseTypeSyntax, formattedNode as PrimaryConstructorBaseTypeSyntax);
            case PropertyDeclarationSyntax propertyDeclarationSyntax:
                return ComparePropertyDeclarationSyntax(propertyDeclarationSyntax, formattedNode as PropertyDeclarationSyntax);
            case PropertyPatternClauseSyntax propertyPatternClauseSyntax:
                return ComparePropertyPatternClauseSyntax(propertyPatternClauseSyntax, formattedNode as PropertyPatternClauseSyntax);
            case QualifiedCrefSyntax qualifiedCrefSyntax:
                return CompareQualifiedCrefSyntax(qualifiedCrefSyntax, formattedNode as QualifiedCrefSyntax);
            case QualifiedNameSyntax qualifiedNameSyntax:
                return CompareQualifiedNameSyntax(qualifiedNameSyntax, formattedNode as QualifiedNameSyntax);
            case QueryBodySyntax queryBodySyntax:
                return CompareQueryBodySyntax(queryBodySyntax, formattedNode as QueryBodySyntax);
            case QueryContinuationSyntax queryContinuationSyntax:
                return CompareQueryContinuationSyntax(queryContinuationSyntax, formattedNode as QueryContinuationSyntax);
            case QueryExpressionSyntax queryExpressionSyntax:
                return CompareQueryExpressionSyntax(queryExpressionSyntax, formattedNode as QueryExpressionSyntax);
            case RangeExpressionSyntax rangeExpressionSyntax:
                return CompareRangeExpressionSyntax(rangeExpressionSyntax, formattedNode as RangeExpressionSyntax);
            case RecordDeclarationSyntax recordDeclarationSyntax:
                return CompareRecordDeclarationSyntax(recordDeclarationSyntax, formattedNode as RecordDeclarationSyntax);
            case RecursivePatternSyntax recursivePatternSyntax:
                return CompareRecursivePatternSyntax(recursivePatternSyntax, formattedNode as RecursivePatternSyntax);
            case ReferenceDirectiveTriviaSyntax referenceDirectiveTriviaSyntax:
                return CompareReferenceDirectiveTriviaSyntax(referenceDirectiveTriviaSyntax, formattedNode as ReferenceDirectiveTriviaSyntax);
            case RefExpressionSyntax refExpressionSyntax:
                return CompareRefExpressionSyntax(refExpressionSyntax, formattedNode as RefExpressionSyntax);
            case RefStructConstraintSyntax refStructConstraintSyntax:
                return CompareRefStructConstraintSyntax(refStructConstraintSyntax, formattedNode as RefStructConstraintSyntax);
            case RefTypeExpressionSyntax refTypeExpressionSyntax:
                return CompareRefTypeExpressionSyntax(refTypeExpressionSyntax, formattedNode as RefTypeExpressionSyntax);
            case RefTypeSyntax refTypeSyntax:
                return CompareRefTypeSyntax(refTypeSyntax, formattedNode as RefTypeSyntax);
            case RefValueExpressionSyntax refValueExpressionSyntax:
                return CompareRefValueExpressionSyntax(refValueExpressionSyntax, formattedNode as RefValueExpressionSyntax);
            case RegionDirectiveTriviaSyntax regionDirectiveTriviaSyntax:
                return CompareRegionDirectiveTriviaSyntax(regionDirectiveTriviaSyntax, formattedNode as RegionDirectiveTriviaSyntax);
            case RelationalPatternSyntax relationalPatternSyntax:
                return CompareRelationalPatternSyntax(relationalPatternSyntax, formattedNode as RelationalPatternSyntax);
            case ReturnStatementSyntax returnStatementSyntax:
                return CompareReturnStatementSyntax(returnStatementSyntax, formattedNode as ReturnStatementSyntax);
            case ScopedTypeSyntax scopedTypeSyntax:
                return CompareScopedTypeSyntax(scopedTypeSyntax, formattedNode as ScopedTypeSyntax);
            case SelectClauseSyntax selectClauseSyntax:
                return CompareSelectClauseSyntax(selectClauseSyntax, formattedNode as SelectClauseSyntax);
            case ShebangDirectiveTriviaSyntax shebangDirectiveTriviaSyntax:
                return CompareShebangDirectiveTriviaSyntax(shebangDirectiveTriviaSyntax, formattedNode as ShebangDirectiveTriviaSyntax);
            case SimpleBaseTypeSyntax simpleBaseTypeSyntax:
                return CompareSimpleBaseTypeSyntax(simpleBaseTypeSyntax, formattedNode as SimpleBaseTypeSyntax);
            case SimpleLambdaExpressionSyntax simpleLambdaExpressionSyntax:
                return CompareSimpleLambdaExpressionSyntax(simpleLambdaExpressionSyntax, formattedNode as SimpleLambdaExpressionSyntax);
            case SingleVariableDesignationSyntax singleVariableDesignationSyntax:
                return CompareSingleVariableDesignationSyntax(singleVariableDesignationSyntax, formattedNode as SingleVariableDesignationSyntax);
            case SizeOfExpressionSyntax sizeOfExpressionSyntax:
                return CompareSizeOfExpressionSyntax(sizeOfExpressionSyntax, formattedNode as SizeOfExpressionSyntax);
            case SkippedTokensTriviaSyntax skippedTokensTriviaSyntax:
                return CompareSkippedTokensTriviaSyntax(skippedTokensTriviaSyntax, formattedNode as SkippedTokensTriviaSyntax);
            case SlicePatternSyntax slicePatternSyntax:
                return CompareSlicePatternSyntax(slicePatternSyntax, formattedNode as SlicePatternSyntax);
            case SpreadElementSyntax spreadElementSyntax:
                return CompareSpreadElementSyntax(spreadElementSyntax, formattedNode as SpreadElementSyntax);
            case StackAllocArrayCreationExpressionSyntax stackAllocArrayCreationExpressionSyntax:
                return CompareStackAllocArrayCreationExpressionSyntax(stackAllocArrayCreationExpressionSyntax, formattedNode as StackAllocArrayCreationExpressionSyntax);
            case StructDeclarationSyntax structDeclarationSyntax:
                return CompareStructDeclarationSyntax(structDeclarationSyntax, formattedNode as StructDeclarationSyntax);
            case SubpatternSyntax subpatternSyntax:
                return CompareSubpatternSyntax(subpatternSyntax, formattedNode as SubpatternSyntax);
            case SwitchExpressionArmSyntax switchExpressionArmSyntax:
                return CompareSwitchExpressionArmSyntax(switchExpressionArmSyntax, formattedNode as SwitchExpressionArmSyntax);
            case SwitchExpressionSyntax switchExpressionSyntax:
                return CompareSwitchExpressionSyntax(switchExpressionSyntax, formattedNode as SwitchExpressionSyntax);
            case SwitchSectionSyntax switchSectionSyntax:
                return CompareSwitchSectionSyntax(switchSectionSyntax, formattedNode as SwitchSectionSyntax);
            case SwitchStatementSyntax switchStatementSyntax:
                return CompareSwitchStatementSyntax(switchStatementSyntax, formattedNode as SwitchStatementSyntax);
            case ThisExpressionSyntax thisExpressionSyntax:
                return CompareThisExpressionSyntax(thisExpressionSyntax, formattedNode as ThisExpressionSyntax);
            case ThrowExpressionSyntax throwExpressionSyntax:
                return CompareThrowExpressionSyntax(throwExpressionSyntax, formattedNode as ThrowExpressionSyntax);
            case ThrowStatementSyntax throwStatementSyntax:
                return CompareThrowStatementSyntax(throwStatementSyntax, formattedNode as ThrowStatementSyntax);
            case TryStatementSyntax tryStatementSyntax:
                return CompareTryStatementSyntax(tryStatementSyntax, formattedNode as TryStatementSyntax);
            case TupleElementSyntax tupleElementSyntax:
                return CompareTupleElementSyntax(tupleElementSyntax, formattedNode as TupleElementSyntax);
            case TupleExpressionSyntax tupleExpressionSyntax:
                return CompareTupleExpressionSyntax(tupleExpressionSyntax, formattedNode as TupleExpressionSyntax);
            case TupleTypeSyntax tupleTypeSyntax:
                return CompareTupleTypeSyntax(tupleTypeSyntax, formattedNode as TupleTypeSyntax);
            case TypeArgumentListSyntax typeArgumentListSyntax:
                return CompareTypeArgumentListSyntax(typeArgumentListSyntax, formattedNode as TypeArgumentListSyntax);
            case TypeConstraintSyntax typeConstraintSyntax:
                return CompareTypeConstraintSyntax(typeConstraintSyntax, formattedNode as TypeConstraintSyntax);
            case TypeCrefSyntax typeCrefSyntax:
                return CompareTypeCrefSyntax(typeCrefSyntax, formattedNode as TypeCrefSyntax);
            case TypeOfExpressionSyntax typeOfExpressionSyntax:
                return CompareTypeOfExpressionSyntax(typeOfExpressionSyntax, formattedNode as TypeOfExpressionSyntax);
            case TypeParameterConstraintClauseSyntax typeParameterConstraintClauseSyntax:
                return CompareTypeParameterConstraintClauseSyntax(typeParameterConstraintClauseSyntax, formattedNode as TypeParameterConstraintClauseSyntax);
            case TypeParameterListSyntax typeParameterListSyntax:
                return CompareTypeParameterListSyntax(typeParameterListSyntax, formattedNode as TypeParameterListSyntax);
            case TypeParameterSyntax typeParameterSyntax:
                return CompareTypeParameterSyntax(typeParameterSyntax, formattedNode as TypeParameterSyntax);
            case TypePatternSyntax typePatternSyntax:
                return CompareTypePatternSyntax(typePatternSyntax, formattedNode as TypePatternSyntax);
            case UnaryPatternSyntax unaryPatternSyntax:
                return CompareUnaryPatternSyntax(unaryPatternSyntax, formattedNode as UnaryPatternSyntax);
            case UndefDirectiveTriviaSyntax undefDirectiveTriviaSyntax:
                return CompareUndefDirectiveTriviaSyntax(undefDirectiveTriviaSyntax, formattedNode as UndefDirectiveTriviaSyntax);
            case UnsafeStatementSyntax unsafeStatementSyntax:
                return CompareUnsafeStatementSyntax(unsafeStatementSyntax, formattedNode as UnsafeStatementSyntax);
            case UsingDirectiveSyntax usingDirectiveSyntax:
                if (ReorderedUsingsWithDisabledText)
                    return default;

                return CompareUsingDirectiveSyntax(usingDirectiveSyntax, formattedNode as UsingDirectiveSyntax);
            case UsingStatementSyntax usingStatementSyntax:
                return CompareUsingStatementSyntax(usingStatementSyntax, formattedNode as UsingStatementSyntax);
            case VariableDeclarationSyntax variableDeclarationSyntax:
                return CompareVariableDeclarationSyntax(variableDeclarationSyntax, formattedNode as VariableDeclarationSyntax);
            case VariableDeclaratorSyntax variableDeclaratorSyntax:
                return CompareVariableDeclaratorSyntax(variableDeclaratorSyntax, formattedNode as VariableDeclaratorSyntax);
            case VarPatternSyntax varPatternSyntax:
                return CompareVarPatternSyntax(varPatternSyntax, formattedNode as VarPatternSyntax);
            case WarningDirectiveTriviaSyntax warningDirectiveTriviaSyntax:
                return CompareWarningDirectiveTriviaSyntax(warningDirectiveTriviaSyntax, formattedNode as WarningDirectiveTriviaSyntax);
            case WhenClauseSyntax whenClauseSyntax:
                return CompareWhenClauseSyntax(whenClauseSyntax, formattedNode as WhenClauseSyntax);
            case WhereClauseSyntax whereClauseSyntax:
                return CompareWhereClauseSyntax(whereClauseSyntax, formattedNode as WhereClauseSyntax);
            case WhileStatementSyntax whileStatementSyntax:
                return CompareWhileStatementSyntax(whileStatementSyntax, formattedNode as WhileStatementSyntax);
            case WithExpressionSyntax withExpressionSyntax:
                return CompareWithExpressionSyntax(withExpressionSyntax, formattedNode as WithExpressionSyntax);
            case XmlCDataSectionSyntax xmlCDataSectionSyntax:
                return CompareXmlCDataSectionSyntax(xmlCDataSectionSyntax, formattedNode as XmlCDataSectionSyntax);
            case XmlCommentSyntax xmlCommentSyntax:
                return CompareXmlCommentSyntax(xmlCommentSyntax, formattedNode as XmlCommentSyntax);
            case XmlCrefAttributeSyntax xmlCrefAttributeSyntax:
                return CompareXmlCrefAttributeSyntax(xmlCrefAttributeSyntax, formattedNode as XmlCrefAttributeSyntax);
            case XmlElementEndTagSyntax xmlElementEndTagSyntax:
                return CompareXmlElementEndTagSyntax(xmlElementEndTagSyntax, formattedNode as XmlElementEndTagSyntax);
            case XmlElementStartTagSyntax xmlElementStartTagSyntax:
                return CompareXmlElementStartTagSyntax(xmlElementStartTagSyntax, formattedNode as XmlElementStartTagSyntax);
            case XmlElementSyntax xmlElementSyntax:
                return CompareXmlElementSyntax(xmlElementSyntax, formattedNode as XmlElementSyntax);
            case XmlEmptyElementSyntax xmlEmptyElementSyntax:
                return CompareXmlEmptyElementSyntax(xmlEmptyElementSyntax, formattedNode as XmlEmptyElementSyntax);
            case XmlNameAttributeSyntax xmlNameAttributeSyntax:
                return CompareXmlNameAttributeSyntax(xmlNameAttributeSyntax, formattedNode as XmlNameAttributeSyntax);
            case XmlNameSyntax xmlNameSyntax:
                return CompareXmlNameSyntax(xmlNameSyntax, formattedNode as XmlNameSyntax);
            case XmlPrefixSyntax xmlPrefixSyntax:
                return CompareXmlPrefixSyntax(xmlPrefixSyntax, formattedNode as XmlPrefixSyntax);
            case XmlProcessingInstructionSyntax xmlProcessingInstructionSyntax:
                return CompareXmlProcessingInstructionSyntax(xmlProcessingInstructionSyntax, formattedNode as XmlProcessingInstructionSyntax);
            case XmlTextAttributeSyntax xmlTextAttributeSyntax:
                return CompareXmlTextAttributeSyntax(xmlTextAttributeSyntax, formattedNode as XmlTextAttributeSyntax);
            case XmlTextSyntax xmlTextSyntax:
                return CompareXmlTextSyntax(xmlTextSyntax, formattedNode as XmlTextSyntax);
            case YieldStatementSyntax yieldStatementSyntax:
                return CompareYieldStatementSyntax(yieldStatementSyntax, formattedNode as YieldStatementSyntax);
            default:
#if DEBUG
                throw new Exception("Can't handle " + originalNode.GetType().Name + ". May need to rerun CSharpier.FakeGenerators");
#else
                return default;
#endif
        }
    }

    private CompareResult CompareAccessorDeclarationSyntax(AccessorDeclarationSyntax originalNode, AccessorDeclarationSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Body, originalNode));
        formattedStack.Push((formattedNode.Body, formattedNode));
        originalStack.Push((originalNode.ExpressionBody, originalNode));
        formattedStack.Push((formattedNode.ExpressionBody, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareAccessorListSyntax(AccessorListSyntax originalNode, AccessorListSyntax formattedNode)
    {
        CompareResult result = CompareLists(originalNode.Accessors, formattedNode.Accessors, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseBraceToken, formattedNode.CloseBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBraceToken, formattedNode.OpenBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareAliasQualifiedNameSyntax(AliasQualifiedNameSyntax originalNode, AliasQualifiedNameSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Alias, originalNode));
        formattedStack.Push((formattedNode.Alias, formattedNode));
        result = Compare(originalNode.ColonColonToken, formattedNode.ColonColonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNint != formattedNode.IsNint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNotNull != formattedNode.IsNotNull)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNuint != formattedNode.IsNuint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsUnmanaged != formattedNode.IsUnmanaged)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsVar != formattedNode.IsVar)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        return default;
    }

    private CompareResult CompareAllowsConstraintClauseSyntax(AllowsConstraintClauseSyntax originalNode, AllowsConstraintClauseSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.AllowsKeyword, formattedNode.AllowsKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Constraints, formattedNode.Constraints, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Constraints),
            AllSeparatorsButLast(formattedNode.Constraints),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareAnonymousMethodExpressionSyntax(AnonymousMethodExpressionSyntax originalNode, AnonymousMethodExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.AsyncKeyword, formattedNode.AsyncKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Block, originalNode));
        formattedStack.Push((formattedNode.Block, formattedNode));
        originalStack.Push((originalNode.Body, originalNode));
        formattedStack.Push((formattedNode.Body, formattedNode));
        result = Compare(originalNode.DelegateKeyword, formattedNode.DelegateKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ExpressionBody, originalNode));
        formattedStack.Push((formattedNode.ExpressionBody, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ParameterList, originalNode));
        formattedStack.Push((formattedNode.ParameterList, formattedNode));
        return default;
    }

    private CompareResult CompareAnonymousObjectCreationExpressionSyntax(
        AnonymousObjectCreationExpressionSyntax originalNode,
        AnonymousObjectCreationExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseBraceToken, formattedNode.CloseBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Initializers, formattedNode.Initializers, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Initializers),
            AllSeparatorsButLast(formattedNode.Initializers),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.NewKeyword, formattedNode.NewKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenBraceToken, formattedNode.OpenBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareAnonymousObjectMemberDeclaratorSyntax(AnonymousObjectMemberDeclaratorSyntax originalNode, AnonymousObjectMemberDeclaratorSyntax formattedNode)
    {
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.NameEquals, originalNode));
        formattedStack.Push((formattedNode.NameEquals, formattedNode));
        return default;
    }

    private CompareResult CompareArgumentListSyntax(ArgumentListSyntax originalNode, ArgumentListSyntax formattedNode)
    {
        CompareResult result = CompareLists(originalNode.Arguments, formattedNode.Arguments, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Arguments),
            AllSeparatorsButLast(formattedNode.Arguments),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareArgumentSyntax(ArgumentSyntax originalNode, ArgumentSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.NameColon, originalNode));
        formattedStack.Push((formattedNode.NameColon, formattedNode));
        result = Compare(originalNode.RefKindKeyword, formattedNode.RefKindKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.RefOrOutKeyword, formattedNode.RefOrOutKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareArrayCreationExpressionSyntax(ArrayCreationExpressionSyntax originalNode, ArrayCreationExpressionSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Initializer, originalNode));
        formattedStack.Push((formattedNode.Initializer, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.NewKeyword, formattedNode.NewKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareArrayRankSpecifierSyntax(ArrayRankSpecifierSyntax originalNode, ArrayRankSpecifierSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseBracketToken, formattedNode.CloseBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBracketToken, formattedNode.OpenBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.Rank != formattedNode.Rank)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(originalNode.Sizes, formattedNode.Sizes, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(AllSeparatorsButLast(originalNode.Sizes), AllSeparatorsButLast(formattedNode.Sizes), CompareFunc, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareArrayTypeSyntax(ArrayTypeSyntax originalNode, ArrayTypeSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.ElementType, originalNode));
        formattedStack.Push((formattedNode.ElementType, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNint != formattedNode.IsNint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNotNull != formattedNode.IsNotNull)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNuint != formattedNode.IsNuint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsUnmanaged != formattedNode.IsUnmanaged)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsVar != formattedNode.IsVar)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(originalNode.RankSpecifiers, formattedNode.RankSpecifiers, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareArrowExpressionClauseSyntax(ArrowExpressionClauseSyntax originalNode, ArrowExpressionClauseSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ArrowToken, formattedNode.ArrowToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareAssignmentExpressionSyntax(AssignmentExpressionSyntax originalNode, AssignmentExpressionSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Left, originalNode));
        formattedStack.Push((formattedNode.Left, formattedNode));
        result = Compare(originalNode.OperatorToken, formattedNode.OperatorToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Right, originalNode));
        formattedStack.Push((formattedNode.Right, formattedNode));
        return default;
    }

    private CompareResult CompareAttributeArgumentListSyntax(AttributeArgumentListSyntax originalNode, AttributeArgumentListSyntax formattedNode)
    {
        CompareResult result = CompareLists(originalNode.Arguments, formattedNode.Arguments, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Arguments),
            AllSeparatorsButLast(formattedNode.Arguments),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareAttributeArgumentSyntax(AttributeArgumentSyntax originalNode, AttributeArgumentSyntax formattedNode)
    {
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.NameColon, originalNode));
        formattedStack.Push((formattedNode.NameColon, formattedNode));
        originalStack.Push((originalNode.NameEquals, originalNode));
        formattedStack.Push((formattedNode.NameEquals, formattedNode));
        return default;
    }

    private CompareResult CompareAttributeListSyntax(AttributeListSyntax originalNode, AttributeListSyntax formattedNode)
    {
        CompareResult result = CompareLists(originalNode.Attributes, formattedNode.Attributes, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Attributes),
            AllSeparatorsButLast(formattedNode.Attributes),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseBracketToken, formattedNode.CloseBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBracketToken, formattedNode.OpenBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Target, originalNode));
        formattedStack.Push((formattedNode.Target, formattedNode));
        return default;
    }

    private CompareResult CompareAttributeSyntax(AttributeSyntax originalNode, AttributeSyntax formattedNode)
    {
        originalStack.Push((originalNode.ArgumentList, originalNode));
        formattedStack.Push((formattedNode.ArgumentList, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        return default;
    }

    private CompareResult CompareAttributeTargetSpecifierSyntax(AttributeTargetSpecifierSyntax originalNode, AttributeTargetSpecifierSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ColonToken, formattedNode.ColonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareAwaitExpressionSyntax(AwaitExpressionSyntax originalNode, AwaitExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.AwaitKeyword, formattedNode.AwaitKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareBadDirectiveTriviaSyntax(BadDirectiveTriviaSyntax originalNode, BadDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareBaseExpressionSyntax(BaseExpressionSyntax originalNode, BaseExpressionSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Token, formattedNode.Token, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareBaseListSyntax(BaseListSyntax originalNode, BaseListSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ColonToken, formattedNode.ColonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(originalNode.Types, formattedNode.Types, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(AllSeparatorsButLast(originalNode.Types), AllSeparatorsButLast(formattedNode.Types), CompareFunc, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareBinaryExpressionSyntax(BinaryExpressionSyntax originalNode, BinaryExpressionSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Left, originalNode));
        formattedStack.Push((formattedNode.Left, formattedNode));
        result = Compare(originalNode.OperatorToken, formattedNode.OperatorToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Right, originalNode));
        formattedStack.Push((formattedNode.Right, formattedNode));
        return default;
    }

    private CompareResult CompareBinaryPatternSyntax(BinaryPatternSyntax originalNode, BinaryPatternSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Left, originalNode));
        formattedStack.Push((formattedNode.Left, formattedNode));
        result = Compare(originalNode.OperatorToken, formattedNode.OperatorToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Right, originalNode));
        formattedStack.Push((formattedNode.Right, formattedNode));
        return default;
    }

    private CompareResult CompareBlockSyntax(BlockSyntax originalNode, BlockSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseBraceToken, formattedNode.CloseBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBraceToken, formattedNode.OpenBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Statements, formattedNode.Statements, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareBracketedArgumentListSyntax(BracketedArgumentListSyntax originalNode, BracketedArgumentListSyntax formattedNode)
    {
        CompareResult result = CompareLists(originalNode.Arguments, formattedNode.Arguments, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Arguments),
            AllSeparatorsButLast(formattedNode.Arguments),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseBracketToken, formattedNode.CloseBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBracketToken, formattedNode.OpenBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareBracketedParameterListSyntax(BracketedParameterListSyntax originalNode, BracketedParameterListSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseBracketToken, formattedNode.CloseBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBracketToken, formattedNode.OpenBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Parameters, formattedNode.Parameters, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Parameters),
            AllSeparatorsButLast(formattedNode.Parameters),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareBreakStatementSyntax(BreakStatementSyntax originalNode, BreakStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.BreakKeyword, formattedNode.BreakKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareCasePatternSwitchLabelSyntax(CasePatternSwitchLabelSyntax originalNode, CasePatternSwitchLabelSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ColonToken, formattedNode.ColonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Pattern, originalNode));
        formattedStack.Push((formattedNode.Pattern, formattedNode));
        originalStack.Push((originalNode.WhenClause, originalNode));
        formattedStack.Push((formattedNode.WhenClause, formattedNode));
        return default;
    }

    private CompareResult CompareCaseSwitchLabelSyntax(CaseSwitchLabelSyntax originalNode, CaseSwitchLabelSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ColonToken, formattedNode.ColonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Value, originalNode));
        formattedStack.Push((formattedNode.Value, formattedNode));
        return default;
    }

    private CompareResult CompareCastExpressionSyntax(CastExpressionSyntax originalNode, CastExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareCatchClauseSyntax(CatchClauseSyntax originalNode, CatchClauseSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Block, originalNode));
        formattedStack.Push((formattedNode.Block, formattedNode));
        result = Compare(originalNode.CatchKeyword, formattedNode.CatchKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Declaration, originalNode));
        formattedStack.Push((formattedNode.Declaration, formattedNode));
        originalStack.Push((originalNode.Filter, originalNode));
        formattedStack.Push((formattedNode.Filter, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareCatchDeclarationSyntax(CatchDeclarationSyntax originalNode, CatchDeclarationSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareCatchFilterClauseSyntax(CatchFilterClauseSyntax originalNode, CatchFilterClauseSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.FilterExpression, originalNode));
        formattedStack.Push((formattedNode.FilterExpression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.WhenKeyword, formattedNode.WhenKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareCheckedExpressionSyntax(CheckedExpressionSyntax originalNode, CheckedExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareCheckedStatementSyntax(CheckedStatementSyntax originalNode, CheckedStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Block, originalNode));
        formattedStack.Push((formattedNode.Block, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareClassDeclarationSyntax(ClassDeclarationSyntax originalNode, ClassDeclarationSyntax formattedNode)
    {
        CompareResult result;
        if (CompareFullSpan(originalNode, formattedNode))
            return default;

        result = CompareLists(originalNode.AttributeLists, formattedNode.AttributeLists, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.BaseList, originalNode));
        formattedStack.Push((formattedNode.BaseList, formattedNode));
        result = Compare(originalNode.CloseBraceToken, formattedNode.CloseBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.ConstraintClauses, formattedNode.ConstraintClauses, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Members, formattedNode.Members, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenBraceToken, formattedNode.OpenBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ParameterList, originalNode));
        formattedStack.Push((formattedNode.ParameterList, formattedNode));
        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.TypeParameterList, originalNode));
        formattedStack.Push((formattedNode.TypeParameterList, formattedNode));
        return default;
    }

    private CompareResult CompareClassOrStructConstraintSyntax(ClassOrStructConstraintSyntax originalNode, ClassOrStructConstraintSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ClassOrStructKeyword, formattedNode.ClassOrStructKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.QuestionToken, formattedNode.QuestionToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareCollectionExpressionSyntax(CollectionExpressionSyntax originalNode, CollectionExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseBracketToken, formattedNode.CloseBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Elements, formattedNode.Elements, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Elements),
            AllSeparatorsButLast(formattedNode.Elements),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBracketToken, formattedNode.OpenBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareCompilationUnitSyntax(CompilationUnitSyntax originalNode, CompilationUnitSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfFileToken, formattedNode.EndOfFileToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Externs, formattedNode.Externs, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(originalNode.Members, formattedNode.Members, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareUsingDirectives(originalNode.Usings, formattedNode.Usings, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareConditionalAccessExpressionSyntax(ConditionalAccessExpressionSyntax originalNode, ConditionalAccessExpressionSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OperatorToken, formattedNode.OperatorToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.WhenNotNull, originalNode));
        formattedStack.Push((formattedNode.WhenNotNull, formattedNode));
        return default;
    }

    private CompareResult CompareConditionalExpressionSyntax(ConditionalExpressionSyntax originalNode, ConditionalExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ColonToken, formattedNode.ColonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Condition, originalNode));
        formattedStack.Push((formattedNode.Condition, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.QuestionToken, formattedNode.QuestionToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.WhenFalse, originalNode));
        formattedStack.Push((formattedNode.WhenFalse, formattedNode));
        originalStack.Push((originalNode.WhenTrue, originalNode));
        formattedStack.Push((formattedNode.WhenTrue, formattedNode));
        return default;
    }

    private CompareResult CompareConstantPatternSyntax(ConstantPatternSyntax originalNode, ConstantPatternSyntax formattedNode)
    {
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareConstructorConstraintSyntax(ConstructorConstraintSyntax originalNode, ConstructorConstraintSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.NewKeyword, formattedNode.NewKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareConstructorDeclarationSyntax(ConstructorDeclarationSyntax originalNode, ConstructorDeclarationSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Body, originalNode));
        formattedStack.Push((formattedNode.Body, formattedNode));
        originalStack.Push((originalNode.ExpressionBody, originalNode));
        formattedStack.Push((formattedNode.ExpressionBody, formattedNode));
        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Initializer, originalNode));
        formattedStack.Push((formattedNode.Initializer, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ParameterList, originalNode));
        formattedStack.Push((formattedNode.ParameterList, formattedNode));
        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareConstructorInitializerSyntax(ConstructorInitializerSyntax originalNode, ConstructorInitializerSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.ArgumentList, originalNode));
        formattedStack.Push((formattedNode.ArgumentList, formattedNode));
        result = Compare(originalNode.ColonToken, formattedNode.ColonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.ThisOrBaseKeyword, formattedNode.ThisOrBaseKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareContinueStatementSyntax(ContinueStatementSyntax originalNode, ContinueStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.ContinueKeyword, formattedNode.ContinueKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareConversionOperatorDeclarationSyntax(ConversionOperatorDeclarationSyntax originalNode, ConversionOperatorDeclarationSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Body, originalNode));
        formattedStack.Push((formattedNode.Body, formattedNode));
        result = Compare(originalNode.CheckedKeyword, formattedNode.CheckedKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ExplicitInterfaceSpecifier, originalNode));
        formattedStack.Push((formattedNode.ExplicitInterfaceSpecifier, formattedNode));
        originalStack.Push((originalNode.ExpressionBody, originalNode));
        formattedStack.Push((formattedNode.ExpressionBody, formattedNode));
        result = Compare(originalNode.ImplicitOrExplicitKeyword, formattedNode.ImplicitOrExplicitKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OperatorKeyword, formattedNode.OperatorKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ParameterList, originalNode));
        formattedStack.Push((formattedNode.ParameterList, formattedNode));
        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareConversionOperatorMemberCrefSyntax(ConversionOperatorMemberCrefSyntax originalNode, ConversionOperatorMemberCrefSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CheckedKeyword, formattedNode.CheckedKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.ImplicitOrExplicitKeyword, formattedNode.ImplicitOrExplicitKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OperatorKeyword, formattedNode.OperatorKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Parameters, originalNode));
        formattedStack.Push((formattedNode.Parameters, formattedNode));
        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareCrefBracketedParameterListSyntax(CrefBracketedParameterListSyntax originalNode, CrefBracketedParameterListSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseBracketToken, formattedNode.CloseBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBracketToken, formattedNode.OpenBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Parameters, formattedNode.Parameters, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Parameters),
            AllSeparatorsButLast(formattedNode.Parameters),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareCrefParameterListSyntax(CrefParameterListSyntax originalNode, CrefParameterListSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Parameters, formattedNode.Parameters, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Parameters),
            AllSeparatorsButLast(formattedNode.Parameters),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareCrefParameterSyntax(CrefParameterSyntax originalNode, CrefParameterSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.ReadOnlyKeyword, formattedNode.ReadOnlyKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.RefKindKeyword, formattedNode.RefKindKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.RefOrOutKeyword, formattedNode.RefOrOutKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareDeclarationExpressionSyntax(DeclarationExpressionSyntax originalNode, DeclarationExpressionSyntax formattedNode)
    {
        originalStack.Push((originalNode.Designation, originalNode));
        formattedStack.Push((formattedNode.Designation, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareDeclarationPatternSyntax(DeclarationPatternSyntax originalNode, DeclarationPatternSyntax formattedNode)
    {
        originalStack.Push((originalNode.Designation, originalNode));
        formattedStack.Push((formattedNode.Designation, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareDefaultConstraintSyntax(DefaultConstraintSyntax originalNode, DefaultConstraintSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DefaultKeyword, formattedNode.DefaultKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareDefaultExpressionSyntax(DefaultExpressionSyntax originalNode, DefaultExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareDefaultSwitchLabelSyntax(DefaultSwitchLabelSyntax originalNode, DefaultSwitchLabelSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ColonToken, formattedNode.ColonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareDefineDirectiveTriviaSyntax(DefineDirectiveTriviaSyntax originalNode, DefineDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DefineKeyword, formattedNode.DefineKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Name, formattedNode.Name, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareDelegateDeclarationSyntax(DelegateDeclarationSyntax originalNode, DelegateDeclarationSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.ConstraintClauses, formattedNode.ConstraintClauses, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.DelegateKeyword, formattedNode.DelegateKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ParameterList, originalNode));
        formattedStack.Push((formattedNode.ParameterList, formattedNode));
        originalStack.Push((originalNode.ReturnType, originalNode));
        formattedStack.Push((formattedNode.ReturnType, formattedNode));
        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.TypeParameterList, originalNode));
        formattedStack.Push((formattedNode.TypeParameterList, formattedNode));
        return default;
    }

    private CompareResult CompareDestructorDeclarationSyntax(DestructorDeclarationSyntax originalNode, DestructorDeclarationSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Body, originalNode));
        formattedStack.Push((formattedNode.Body, formattedNode));
        originalStack.Push((originalNode.ExpressionBody, originalNode));
        formattedStack.Push((formattedNode.ExpressionBody, formattedNode));
        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ParameterList, originalNode));
        formattedStack.Push((formattedNode.ParameterList, formattedNode));
        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.TildeToken, formattedNode.TildeToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareDiscardDesignationSyntax(DiscardDesignationSyntax originalNode, DiscardDesignationSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.UnderscoreToken, formattedNode.UnderscoreToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareDiscardPatternSyntax(DiscardPatternSyntax originalNode, DiscardPatternSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.UnderscoreToken, formattedNode.UnderscoreToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareDocumentationCommentTriviaSyntax(DocumentationCommentTriviaSyntax originalNode, DocumentationCommentTriviaSyntax formattedNode)
    {
        CompareResult result = CompareLists(originalNode.Content, formattedNode.Content, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfComment, formattedNode.EndOfComment, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareDoStatementSyntax(DoStatementSyntax originalNode, DoStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Condition, originalNode));
        formattedStack.Push((formattedNode.Condition, formattedNode));
        result = Compare(originalNode.DoKeyword, formattedNode.DoKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Statement, originalNode));
        formattedStack.Push((formattedNode.Statement, formattedNode));
        result = Compare(originalNode.WhileKeyword, formattedNode.WhileKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareElementAccessExpressionSyntax(ElementAccessExpressionSyntax originalNode, ElementAccessExpressionSyntax formattedNode)
    {
        originalStack.Push((originalNode.ArgumentList, originalNode));
        formattedStack.Push((formattedNode.ArgumentList, formattedNode));
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareElementBindingExpressionSyntax(ElementBindingExpressionSyntax originalNode, ElementBindingExpressionSyntax formattedNode)
    {
        originalStack.Push((originalNode.ArgumentList, originalNode));
        formattedStack.Push((formattedNode.ArgumentList, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareElifDirectiveTriviaSyntax(ElifDirectiveTriviaSyntax originalNode, ElifDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.BranchTaken != formattedNode.BranchTaken)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Condition, originalNode));
        formattedStack.Push((formattedNode.Condition, formattedNode));
        if (originalNode.ConditionValue != formattedNode.ConditionValue)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.ElifKeyword, formattedNode.ElifKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareElseClauseSyntax(ElseClauseSyntax originalNode, ElseClauseSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ElseKeyword, formattedNode.ElseKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Statement, originalNode));
        formattedStack.Push((formattedNode.Statement, formattedNode));
        return default;
    }

    private CompareResult CompareElseDirectiveTriviaSyntax(ElseDirectiveTriviaSyntax originalNode, ElseDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.BranchTaken != formattedNode.BranchTaken)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.ElseKeyword, formattedNode.ElseKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareEmptyStatementSyntax(EmptyStatementSyntax originalNode, EmptyStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareEndIfDirectiveTriviaSyntax(EndIfDirectiveTriviaSyntax originalNode, EndIfDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndIfKeyword, formattedNode.EndIfKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareEndRegionDirectiveTriviaSyntax(EndRegionDirectiveTriviaSyntax originalNode, EndRegionDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndRegionKeyword, formattedNode.EndRegionKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareEnumDeclarationSyntax(EnumDeclarationSyntax originalNode, EnumDeclarationSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.BaseList, originalNode));
        formattedStack.Push((formattedNode.BaseList, formattedNode));
        result = Compare(originalNode.CloseBraceToken, formattedNode.CloseBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EnumKeyword, formattedNode.EnumKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(originalNode.Members, formattedNode.Members, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Members),
            AllSeparatorsButLast(formattedNode.Members),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenBraceToken, formattedNode.OpenBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareEnumMemberDeclarationSyntax(EnumMemberDeclarationSyntax originalNode, EnumMemberDeclarationSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.EqualsValue, originalNode));
        formattedStack.Push((formattedNode.EqualsValue, formattedNode));
        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareEqualsValueClauseSyntax(EqualsValueClauseSyntax originalNode, EqualsValueClauseSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.EqualsToken, formattedNode.EqualsToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Value, originalNode));
        formattedStack.Push((formattedNode.Value, formattedNode));
        return default;
    }

    private CompareResult CompareErrorDirectiveTriviaSyntax(ErrorDirectiveTriviaSyntax originalNode, ErrorDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.ErrorKeyword, formattedNode.ErrorKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareEventDeclarationSyntax(EventDeclarationSyntax originalNode, EventDeclarationSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.AccessorList, originalNode));
        formattedStack.Push((formattedNode.AccessorList, formattedNode));
        result = CompareLists(originalNode.AttributeLists, formattedNode.AttributeLists, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EventKeyword, formattedNode.EventKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ExplicitInterfaceSpecifier, originalNode));
        formattedStack.Push((formattedNode.ExplicitInterfaceSpecifier, formattedNode));
        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareEventFieldDeclarationSyntax(EventFieldDeclarationSyntax originalNode, EventFieldDeclarationSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Declaration, originalNode));
        formattedStack.Push((formattedNode.Declaration, formattedNode));
        result = Compare(originalNode.EventKeyword, formattedNode.EventKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareExplicitInterfaceSpecifierSyntax(ExplicitInterfaceSpecifierSyntax originalNode, ExplicitInterfaceSpecifierSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DotToken, formattedNode.DotToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        return default;
    }

    private CompareResult CompareExpressionColonSyntax(ExpressionColonSyntax originalNode, ExpressionColonSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ColonToken, formattedNode.ColonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareExpressionElementSyntax(ExpressionElementSyntax originalNode, ExpressionElementSyntax formattedNode)
    {
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareExpressionStatementSyntax(ExpressionStatementSyntax originalNode, ExpressionStatementSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.AllowsAnyExpression != formattedNode.AllowsAnyExpression)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(originalNode.AttributeLists, formattedNode.AttributeLists, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareExternAliasDirectiveSyntax(ExternAliasDirectiveSyntax originalNode, ExternAliasDirectiveSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.AliasKeyword, formattedNode.AliasKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.ExternKeyword, formattedNode.ExternKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareFieldDeclarationSyntax(FieldDeclarationSyntax originalNode, FieldDeclarationSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Declaration, originalNode));
        formattedStack.Push((formattedNode.Declaration, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareFieldExpressionSyntax(FieldExpressionSyntax originalNode, FieldExpressionSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Token, formattedNode.Token, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareFileScopedNamespaceDeclarationSyntax(FileScopedNamespaceDeclarationSyntax originalNode, FileScopedNamespaceDeclarationSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Externs, formattedNode.Externs, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(originalNode.Members, formattedNode.Members, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        result = Compare(originalNode.NamespaceKeyword, formattedNode.NamespaceKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareUsingDirectives(originalNode.Usings, formattedNode.Usings, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareFinallyClauseSyntax(FinallyClauseSyntax originalNode, FinallyClauseSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Block, originalNode));
        formattedStack.Push((formattedNode.Block, formattedNode));
        result = Compare(originalNode.FinallyKeyword, formattedNode.FinallyKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareFixedStatementSyntax(FixedStatementSyntax originalNode, FixedStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Declaration, originalNode));
        formattedStack.Push((formattedNode.Declaration, formattedNode));
        result = Compare(originalNode.FixedKeyword, formattedNode.FixedKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Statement, originalNode));
        formattedStack.Push((formattedNode.Statement, formattedNode));
        return default;
    }

    private CompareResult CompareForEachStatementSyntax(ForEachStatementSyntax originalNode, ForEachStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.AwaitKeyword, formattedNode.AwaitKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        result = Compare(originalNode.ForEachKeyword, formattedNode.ForEachKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.InKeyword, formattedNode.InKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Statement, originalNode));
        formattedStack.Push((formattedNode.Statement, formattedNode));
        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareForEachVariableStatementSyntax(ForEachVariableStatementSyntax originalNode, ForEachVariableStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.AwaitKeyword, formattedNode.AwaitKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        result = Compare(originalNode.ForEachKeyword, formattedNode.ForEachKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.InKeyword, formattedNode.InKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Statement, originalNode));
        formattedStack.Push((formattedNode.Statement, formattedNode));
        originalStack.Push((originalNode.Variable, originalNode));
        formattedStack.Push((formattedNode.Variable, formattedNode));
        return default;
    }

    private CompareResult CompareForStatementSyntax(ForStatementSyntax originalNode, ForStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Condition, originalNode));
        formattedStack.Push((formattedNode.Condition, formattedNode));
        originalStack.Push((originalNode.Declaration, originalNode));
        formattedStack.Push((formattedNode.Declaration, formattedNode));
        result = Compare(originalNode.FirstSemicolonToken, formattedNode.FirstSemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.ForKeyword, formattedNode.ForKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Incrementors, formattedNode.Incrementors, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Incrementors),
            AllSeparatorsButLast(formattedNode.Incrementors),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Initializers, formattedNode.Initializers, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Initializers),
            AllSeparatorsButLast(formattedNode.Initializers),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SecondSemicolonToken, formattedNode.SecondSemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Statement, originalNode));
        formattedStack.Push((formattedNode.Statement, formattedNode));
        return default;
    }

    private CompareResult CompareFromClauseSyntax(FromClauseSyntax originalNode, FromClauseSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        result = Compare(originalNode.FromKeyword, formattedNode.FromKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.InKeyword, formattedNode.InKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareFunctionPointerCallingConventionSyntax(FunctionPointerCallingConventionSyntax originalNode, FunctionPointerCallingConventionSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.ManagedOrUnmanagedKeyword, formattedNode.ManagedOrUnmanagedKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.UnmanagedCallingConventionList, originalNode));
        formattedStack.Push((formattedNode.UnmanagedCallingConventionList, formattedNode));
        return default;
    }

    private CompareResult CompareFunctionPointerParameterListSyntax(FunctionPointerParameterListSyntax originalNode, FunctionPointerParameterListSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.GreaterThanToken, formattedNode.GreaterThanToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.LessThanToken, formattedNode.LessThanToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Parameters, formattedNode.Parameters, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Parameters),
            AllSeparatorsButLast(formattedNode.Parameters),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareFunctionPointerParameterSyntax(FunctionPointerParameterSyntax originalNode, FunctionPointerParameterSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareFunctionPointerTypeSyntax(FunctionPointerTypeSyntax originalNode, FunctionPointerTypeSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.AsteriskToken, formattedNode.AsteriskToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.CallingConvention, originalNode));
        formattedStack.Push((formattedNode.CallingConvention, formattedNode));
        result = Compare(originalNode.DelegateKeyword, formattedNode.DelegateKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNint != formattedNode.IsNint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNotNull != formattedNode.IsNotNull)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNuint != formattedNode.IsNuint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsUnmanaged != formattedNode.IsUnmanaged)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsVar != formattedNode.IsVar)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.ParameterList, originalNode));
        formattedStack.Push((formattedNode.ParameterList, formattedNode));
        return default;
    }

    private CompareResult CompareFunctionPointerUnmanagedCallingConventionListSyntax(
        FunctionPointerUnmanagedCallingConventionListSyntax originalNode,
        FunctionPointerUnmanagedCallingConventionListSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.CallingConventions,
            formattedNode.CallingConventions,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.CallingConventions),
            AllSeparatorsButLast(formattedNode.CallingConventions),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseBracketToken, formattedNode.CloseBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBracketToken, formattedNode.OpenBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareFunctionPointerUnmanagedCallingConventionSyntax(
        FunctionPointerUnmanagedCallingConventionSyntax originalNode,
        FunctionPointerUnmanagedCallingConventionSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Name, formattedNode.Name, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareGenericNameSyntax(GenericNameSyntax originalNode, GenericNameSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNint != formattedNode.IsNint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNotNull != formattedNode.IsNotNull)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNuint != formattedNode.IsNuint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsUnboundGenericName != formattedNode.IsUnboundGenericName)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsUnmanaged != formattedNode.IsUnmanaged)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsVar != formattedNode.IsVar)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.TypeArgumentList, originalNode));
        formattedStack.Push((formattedNode.TypeArgumentList, formattedNode));
        return default;
    }

    private CompareResult CompareGlobalStatementSyntax(GlobalStatementSyntax originalNode, GlobalStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Statement, originalNode));
        formattedStack.Push((formattedNode.Statement, formattedNode));
        return default;
    }

    private CompareResult CompareGotoStatementSyntax(GotoStatementSyntax originalNode, GotoStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CaseOrDefaultKeyword, formattedNode.CaseOrDefaultKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        result = Compare(originalNode.GotoKeyword, formattedNode.GotoKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareGroupClauseSyntax(GroupClauseSyntax originalNode, GroupClauseSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.ByExpression, originalNode));
        formattedStack.Push((formattedNode.ByExpression, formattedNode));
        result = Compare(originalNode.ByKeyword, formattedNode.ByKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.GroupExpression, originalNode));
        formattedStack.Push((formattedNode.GroupExpression, formattedNode));
        result = Compare(originalNode.GroupKeyword, formattedNode.GroupKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareIdentifierNameSyntax(IdentifierNameSyntax originalNode, IdentifierNameSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNint != formattedNode.IsNint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNotNull != formattedNode.IsNotNull)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNuint != formattedNode.IsNuint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsUnmanaged != formattedNode.IsUnmanaged)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsVar != formattedNode.IsVar)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareIfDirectiveTriviaSyntax(IfDirectiveTriviaSyntax originalNode, IfDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.BranchTaken != formattedNode.BranchTaken)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Condition, originalNode));
        formattedStack.Push((formattedNode.Condition, formattedNode));
        if (originalNode.ConditionValue != formattedNode.ConditionValue)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.IfKeyword, formattedNode.IfKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareIfStatementSyntax(IfStatementSyntax originalNode, IfStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Condition, originalNode));
        formattedStack.Push((formattedNode.Condition, formattedNode));
        originalStack.Push((originalNode.Else, originalNode));
        formattedStack.Push((formattedNode.Else, formattedNode));
        result = Compare(originalNode.IfKeyword, formattedNode.IfKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Statement, originalNode));
        formattedStack.Push((formattedNode.Statement, formattedNode));
        return default;
    }

    private CompareResult CompareIgnoredDirectiveTriviaSyntax(IgnoredDirectiveTriviaSyntax originalNode, IgnoredDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ColonToken, formattedNode.ColonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareImplicitArrayCreationExpressionSyntax(ImplicitArrayCreationExpressionSyntax originalNode, ImplicitArrayCreationExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseBracketToken, formattedNode.CloseBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Commas, formattedNode.Commas, CompareFunc, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Initializer, originalNode));
        formattedStack.Push((formattedNode.Initializer, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.NewKeyword, formattedNode.NewKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenBracketToken, formattedNode.OpenBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareImplicitElementAccessSyntax(ImplicitElementAccessSyntax originalNode, ImplicitElementAccessSyntax formattedNode)
    {
        originalStack.Push((originalNode.ArgumentList, originalNode));
        formattedStack.Push((formattedNode.ArgumentList, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareImplicitObjectCreationExpressionSyntax(ImplicitObjectCreationExpressionSyntax originalNode, ImplicitObjectCreationExpressionSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.ArgumentList, originalNode));
        formattedStack.Push((formattedNode.ArgumentList, formattedNode));
        originalStack.Push((originalNode.Initializer, originalNode));
        formattedStack.Push((formattedNode.Initializer, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.NewKeyword, formattedNode.NewKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareImplicitStackAllocArrayCreationExpressionSyntax(
        ImplicitStackAllocArrayCreationExpressionSyntax originalNode,
        ImplicitStackAllocArrayCreationExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseBracketToken, formattedNode.CloseBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Initializer, originalNode));
        formattedStack.Push((formattedNode.Initializer, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBracketToken, formattedNode.OpenBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.StackAllocKeyword, formattedNode.StackAllocKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareIncompleteMemberSyntax(IncompleteMemberSyntax originalNode, IncompleteMemberSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareIndexerDeclarationSyntax(IndexerDeclarationSyntax originalNode, IndexerDeclarationSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.AccessorList, originalNode));
        formattedStack.Push((formattedNode.AccessorList, formattedNode));
        result = CompareLists(originalNode.AttributeLists, formattedNode.AttributeLists, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ExplicitInterfaceSpecifier, originalNode));
        formattedStack.Push((formattedNode.ExplicitInterfaceSpecifier, formattedNode));
        originalStack.Push((originalNode.ExpressionBody, originalNode));
        formattedStack.Push((formattedNode.ExpressionBody, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ParameterList, originalNode));
        formattedStack.Push((formattedNode.ParameterList, formattedNode));
        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.ThisKeyword, formattedNode.ThisKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareIndexerMemberCrefSyntax(IndexerMemberCrefSyntax originalNode, IndexerMemberCrefSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Parameters, originalNode));
        formattedStack.Push((formattedNode.Parameters, formattedNode));
        result = Compare(originalNode.ThisKeyword, formattedNode.ThisKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareInitializerExpressionSyntax(InitializerExpressionSyntax originalNode, InitializerExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseBraceToken, formattedNode.CloseBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Expressions, formattedNode.Expressions, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Expressions),
            AllSeparatorsButLast(formattedNode.Expressions),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBraceToken, formattedNode.OpenBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareInterfaceDeclarationSyntax(InterfaceDeclarationSyntax originalNode, InterfaceDeclarationSyntax formattedNode)
    {
        CompareResult result;
        if (CompareFullSpan(originalNode, formattedNode))
            return default;

        result = CompareLists(originalNode.AttributeLists, formattedNode.AttributeLists, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.BaseList, originalNode));
        formattedStack.Push((formattedNode.BaseList, formattedNode));
        result = Compare(originalNode.CloseBraceToken, formattedNode.CloseBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.ConstraintClauses, formattedNode.ConstraintClauses, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Members, formattedNode.Members, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenBraceToken, formattedNode.OpenBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ParameterList, originalNode));
        formattedStack.Push((formattedNode.ParameterList, formattedNode));
        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.TypeParameterList, originalNode));
        formattedStack.Push((formattedNode.TypeParameterList, formattedNode));
        return default;
    }

    private CompareResult CompareInterpolatedStringExpressionSyntax(InterpolatedStringExpressionSyntax originalNode, InterpolatedStringExpressionSyntax formattedNode)
    {
        CompareResult result = CompareLists(originalNode.Contents, formattedNode.Contents, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.StringEndToken, formattedNode.StringEndToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.StringStartToken, formattedNode.StringStartToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareInterpolatedStringTextSyntax(InterpolatedStringTextSyntax originalNode, InterpolatedStringTextSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.TextToken, formattedNode.TextToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareInterpolationAlignmentClauseSyntax(InterpolationAlignmentClauseSyntax originalNode, InterpolationAlignmentClauseSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CommaToken, formattedNode.CommaToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Value, originalNode));
        formattedStack.Push((formattedNode.Value, formattedNode));
        return default;
    }

    private CompareResult CompareInterpolationFormatClauseSyntax(InterpolationFormatClauseSyntax originalNode, InterpolationFormatClauseSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ColonToken, formattedNode.ColonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.FormatStringToken, formattedNode.FormatStringToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareInterpolationSyntax(InterpolationSyntax originalNode, InterpolationSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.AlignmentClause, originalNode));
        formattedStack.Push((formattedNode.AlignmentClause, formattedNode));
        result = Compare(originalNode.CloseBraceToken, formattedNode.CloseBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        originalStack.Push((originalNode.FormatClause, originalNode));
        formattedStack.Push((formattedNode.FormatClause, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBraceToken, formattedNode.OpenBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareInvocationExpressionSyntax(InvocationExpressionSyntax originalNode, InvocationExpressionSyntax formattedNode)
    {
        originalStack.Push((originalNode.ArgumentList, originalNode));
        formattedStack.Push((formattedNode.ArgumentList, formattedNode));
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareIsPatternExpressionSyntax(IsPatternExpressionSyntax originalNode, IsPatternExpressionSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        result = Compare(originalNode.IsKeyword, formattedNode.IsKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Pattern, originalNode));
        formattedStack.Push((formattedNode.Pattern, formattedNode));
        return default;
    }

    private CompareResult CompareJoinClauseSyntax(JoinClauseSyntax originalNode, JoinClauseSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.EqualsKeyword, formattedNode.EqualsKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.InExpression, originalNode));
        formattedStack.Push((formattedNode.InExpression, formattedNode));
        result = Compare(originalNode.InKeyword, formattedNode.InKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Into, originalNode));
        formattedStack.Push((formattedNode.Into, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.JoinKeyword, formattedNode.JoinKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.LeftExpression, originalNode));
        formattedStack.Push((formattedNode.LeftExpression, formattedNode));
        result = Compare(originalNode.OnKeyword, formattedNode.OnKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.RightExpression, originalNode));
        formattedStack.Push((formattedNode.RightExpression, formattedNode));
        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareJoinIntoClauseSyntax(JoinIntoClauseSyntax originalNode, JoinIntoClauseSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.IntoKeyword, formattedNode.IntoKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareLabeledStatementSyntax(LabeledStatementSyntax originalNode, LabeledStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.ColonToken, formattedNode.ColonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Statement, originalNode));
        formattedStack.Push((formattedNode.Statement, formattedNode));
        return default;
    }

    private CompareResult CompareLetClauseSyntax(LetClauseSyntax originalNode, LetClauseSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.EqualsToken, formattedNode.EqualsToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.LetKeyword, formattedNode.LetKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareLineDirectivePositionSyntax(LineDirectivePositionSyntax originalNode, LineDirectivePositionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.Character, formattedNode.Character, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CommaToken, formattedNode.CommaToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Line, formattedNode.Line, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareLineDirectiveTriviaSyntax(LineDirectiveTriviaSyntax originalNode, LineDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.File, formattedNode.File, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Line, formattedNode.Line, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.LineKeyword, formattedNode.LineKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareLineSpanDirectiveTriviaSyntax(LineSpanDirectiveTriviaSyntax originalNode, LineSpanDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CharacterOffset, formattedNode.CharacterOffset, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.End, originalNode));
        formattedStack.Push((formattedNode.End, formattedNode));
        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.File, formattedNode.File, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.LineKeyword, formattedNode.LineKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.MinusToken, formattedNode.MinusToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Start, originalNode));
        formattedStack.Push((formattedNode.Start, formattedNode));
        return default;
    }

    private CompareResult CompareListPatternSyntax(ListPatternSyntax originalNode, ListPatternSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseBracketToken, formattedNode.CloseBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Designation, originalNode));
        formattedStack.Push((formattedNode.Designation, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBracketToken, formattedNode.OpenBracketToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Patterns, formattedNode.Patterns, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Patterns),
            AllSeparatorsButLast(formattedNode.Patterns),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareLiteralExpressionSyntax(LiteralExpressionSyntax originalNode, LiteralExpressionSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Token, formattedNode.Token, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareLoadDirectiveTriviaSyntax(LoadDirectiveTriviaSyntax originalNode, LoadDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.File, formattedNode.File, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.LoadKeyword, formattedNode.LoadKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareLocalDeclarationStatementSyntax(LocalDeclarationStatementSyntax originalNode, LocalDeclarationStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.AwaitKeyword, formattedNode.AwaitKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Declaration, originalNode));
        formattedStack.Push((formattedNode.Declaration, formattedNode));
        if (originalNode.IsConst != formattedNode.IsConst)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.UsingKeyword, formattedNode.UsingKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareLocalFunctionStatementSyntax(LocalFunctionStatementSyntax originalNode, LocalFunctionStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Body, originalNode));
        formattedStack.Push((formattedNode.Body, formattedNode));
        result = CompareLists(originalNode.ConstraintClauses, formattedNode.ConstraintClauses, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ExpressionBody, originalNode));
        formattedStack.Push((formattedNode.ExpressionBody, formattedNode));
        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ParameterList, originalNode));
        formattedStack.Push((formattedNode.ParameterList, formattedNode));
        originalStack.Push((originalNode.ReturnType, originalNode));
        formattedStack.Push((formattedNode.ReturnType, formattedNode));
        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.TypeParameterList, originalNode));
        formattedStack.Push((formattedNode.TypeParameterList, formattedNode));
        return default;
    }

    private CompareResult CompareLockStatementSyntax(LockStatementSyntax originalNode, LockStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.LockKeyword, formattedNode.LockKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Statement, originalNode));
        formattedStack.Push((formattedNode.Statement, formattedNode));
        return default;
    }

    private CompareResult CompareMakeRefExpressionSyntax(MakeRefExpressionSyntax originalNode, MakeRefExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareMemberAccessExpressionSyntax(MemberAccessExpressionSyntax originalNode, MemberAccessExpressionSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        result = Compare(originalNode.OperatorToken, formattedNode.OperatorToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareMemberBindingExpressionSyntax(MemberBindingExpressionSyntax originalNode, MemberBindingExpressionSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        result = Compare(originalNode.OperatorToken, formattedNode.OperatorToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareMethodDeclarationSyntax(MethodDeclarationSyntax originalNode, MethodDeclarationSyntax formattedNode)
    {
        CompareResult result;
        if (CompareFullSpan(originalNode, formattedNode))
            return default;

        result = CompareLists(originalNode.AttributeLists, formattedNode.AttributeLists, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Body, originalNode));
        formattedStack.Push((formattedNode.Body, formattedNode));
        result = CompareLists(originalNode.ConstraintClauses, formattedNode.ConstraintClauses, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ExplicitInterfaceSpecifier, originalNode));
        formattedStack.Push((formattedNode.ExplicitInterfaceSpecifier, formattedNode));
        originalStack.Push((originalNode.ExpressionBody, originalNode));
        formattedStack.Push((formattedNode.ExpressionBody, formattedNode));
        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ParameterList, originalNode));
        formattedStack.Push((formattedNode.ParameterList, formattedNode));
        originalStack.Push((originalNode.ReturnType, originalNode));
        formattedStack.Push((formattedNode.ReturnType, formattedNode));
        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.TypeParameterList, originalNode));
        formattedStack.Push((formattedNode.TypeParameterList, formattedNode));
        return default;
    }

    private CompareResult CompareNameColonSyntax(NameColonSyntax originalNode, NameColonSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ColonToken, formattedNode.ColonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        return default;
    }

    private CompareResult CompareNameEqualsSyntax(NameEqualsSyntax originalNode, NameEqualsSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.EqualsToken, formattedNode.EqualsToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        return default;
    }

    private CompareResult CompareNameMemberCrefSyntax(NameMemberCrefSyntax originalNode, NameMemberCrefSyntax formattedNode)
    {
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        originalStack.Push((originalNode.Parameters, originalNode));
        formattedStack.Push((formattedNode.Parameters, formattedNode));
        return default;
    }

    private CompareResult CompareNamespaceDeclarationSyntax(NamespaceDeclarationSyntax originalNode, NamespaceDeclarationSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseBraceToken, formattedNode.CloseBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Externs, formattedNode.Externs, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(originalNode.Members, formattedNode.Members, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        result = Compare(originalNode.NamespaceKeyword, formattedNode.NamespaceKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenBraceToken, formattedNode.OpenBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareUsingDirectives(originalNode.Usings, formattedNode.Usings, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareNullableDirectiveTriviaSyntax(NullableDirectiveTriviaSyntax originalNode, NullableDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.NullableKeyword, formattedNode.NullableKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SettingToken, formattedNode.SettingToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.TargetToken, formattedNode.TargetToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareNullableTypeSyntax(NullableTypeSyntax originalNode, NullableTypeSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.ElementType, originalNode));
        formattedStack.Push((formattedNode.ElementType, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNint != formattedNode.IsNint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNotNull != formattedNode.IsNotNull)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNuint != formattedNode.IsNuint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsUnmanaged != formattedNode.IsUnmanaged)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsVar != formattedNode.IsVar)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.QuestionToken, formattedNode.QuestionToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareObjectCreationExpressionSyntax(ObjectCreationExpressionSyntax originalNode, ObjectCreationExpressionSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.ArgumentList, originalNode));
        formattedStack.Push((formattedNode.ArgumentList, formattedNode));
        originalStack.Push((originalNode.Initializer, originalNode));
        formattedStack.Push((formattedNode.Initializer, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.NewKeyword, formattedNode.NewKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareOmittedArraySizeExpressionSyntax(OmittedArraySizeExpressionSyntax originalNode, OmittedArraySizeExpressionSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OmittedArraySizeExpressionToken, formattedNode.OmittedArraySizeExpressionToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareOmittedTypeArgumentSyntax(OmittedTypeArgumentSyntax originalNode, OmittedTypeArgumentSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNint != formattedNode.IsNint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNotNull != formattedNode.IsNotNull)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNuint != formattedNode.IsNuint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsUnmanaged != formattedNode.IsUnmanaged)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsVar != formattedNode.IsVar)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OmittedTypeArgumentToken, formattedNode.OmittedTypeArgumentToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareOperatorDeclarationSyntax(OperatorDeclarationSyntax originalNode, OperatorDeclarationSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Body, originalNode));
        formattedStack.Push((formattedNode.Body, formattedNode));
        result = Compare(originalNode.CheckedKeyword, formattedNode.CheckedKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ExplicitInterfaceSpecifier, originalNode));
        formattedStack.Push((formattedNode.ExplicitInterfaceSpecifier, formattedNode));
        originalStack.Push((originalNode.ExpressionBody, originalNode));
        formattedStack.Push((formattedNode.ExpressionBody, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OperatorKeyword, formattedNode.OperatorKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OperatorToken, formattedNode.OperatorToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ParameterList, originalNode));
        formattedStack.Push((formattedNode.ParameterList, formattedNode));
        originalStack.Push((originalNode.ReturnType, originalNode));
        formattedStack.Push((formattedNode.ReturnType, formattedNode));
        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareOperatorMemberCrefSyntax(OperatorMemberCrefSyntax originalNode, OperatorMemberCrefSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CheckedKeyword, formattedNode.CheckedKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OperatorKeyword, formattedNode.OperatorKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OperatorToken, formattedNode.OperatorToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Parameters, originalNode));
        formattedStack.Push((formattedNode.Parameters, formattedNode));
        return default;
    }

    private CompareResult CompareOrderByClauseSyntax(OrderByClauseSyntax originalNode, OrderByClauseSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OrderByKeyword, formattedNode.OrderByKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Orderings, formattedNode.Orderings, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Orderings),
            AllSeparatorsButLast(formattedNode.Orderings),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareOrderingSyntax(OrderingSyntax originalNode, OrderingSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.AscendingOrDescendingKeyword, formattedNode.AscendingOrDescendingKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareParameterListSyntax(ParameterListSyntax originalNode, ParameterListSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Parameters, formattedNode.Parameters, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Parameters),
            AllSeparatorsButLast(formattedNode.Parameters),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareParameterSyntax(ParameterSyntax originalNode, ParameterSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Default, originalNode));
        formattedStack.Push((formattedNode.Default, formattedNode));
        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareParenthesizedExpressionSyntax(ParenthesizedExpressionSyntax originalNode, ParenthesizedExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareParenthesizedLambdaExpressionSyntax(ParenthesizedLambdaExpressionSyntax originalNode, ParenthesizedLambdaExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ArrowToken, formattedNode.ArrowToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.AsyncKeyword, formattedNode.AsyncKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.AttributeLists, formattedNode.AttributeLists, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Block, originalNode));
        formattedStack.Push((formattedNode.Block, formattedNode));
        originalStack.Push((originalNode.Body, originalNode));
        formattedStack.Push((formattedNode.Body, formattedNode));
        originalStack.Push((originalNode.ExpressionBody, originalNode));
        formattedStack.Push((formattedNode.ExpressionBody, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ParameterList, originalNode));
        formattedStack.Push((formattedNode.ParameterList, formattedNode));
        originalStack.Push((originalNode.ReturnType, originalNode));
        formattedStack.Push((formattedNode.ReturnType, formattedNode));
        return default;
    }

    private CompareResult CompareParenthesizedPatternSyntax(ParenthesizedPatternSyntax originalNode, ParenthesizedPatternSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Pattern, originalNode));
        formattedStack.Push((formattedNode.Pattern, formattedNode));
        return default;
    }

    private CompareResult CompareParenthesizedVariableDesignationSyntax(ParenthesizedVariableDesignationSyntax originalNode, ParenthesizedVariableDesignationSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Variables, formattedNode.Variables, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Variables),
            AllSeparatorsButLast(formattedNode.Variables),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult ComparePointerTypeSyntax(PointerTypeSyntax originalNode, PointerTypeSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.AsteriskToken, formattedNode.AsteriskToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ElementType, originalNode));
        formattedStack.Push((formattedNode.ElementType, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNint != formattedNode.IsNint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNotNull != formattedNode.IsNotNull)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNuint != formattedNode.IsNuint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsUnmanaged != formattedNode.IsUnmanaged)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsVar != formattedNode.IsVar)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult ComparePositionalPatternClauseSyntax(PositionalPatternClauseSyntax originalNode, PositionalPatternClauseSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Subpatterns, formattedNode.Subpatterns, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Subpatterns),
            AllSeparatorsButLast(formattedNode.Subpatterns),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult ComparePostfixUnaryExpressionSyntax(PostfixUnaryExpressionSyntax originalNode, PostfixUnaryExpressionSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Operand, originalNode));
        formattedStack.Push((formattedNode.Operand, formattedNode));
        result = Compare(originalNode.OperatorToken, formattedNode.OperatorToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult ComparePragmaChecksumDirectiveTriviaSyntax(PragmaChecksumDirectiveTriviaSyntax originalNode, PragmaChecksumDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.Bytes, formattedNode.Bytes, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.ChecksumKeyword, formattedNode.ChecksumKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.File, formattedNode.File, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Guid, formattedNode.Guid, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.PragmaKeyword, formattedNode.PragmaKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult ComparePragmaWarningDirectiveTriviaSyntax(PragmaWarningDirectiveTriviaSyntax originalNode, PragmaWarningDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.DisableOrRestoreKeyword, formattedNode.DisableOrRestoreKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.ErrorCodes, formattedNode.ErrorCodes, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.ErrorCodes),
            AllSeparatorsButLast(formattedNode.ErrorCodes),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.PragmaKeyword, formattedNode.PragmaKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.WarningKeyword, formattedNode.WarningKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult ComparePredefinedTypeSyntax(PredefinedTypeSyntax originalNode, PredefinedTypeSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNint != formattedNode.IsNint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNotNull != formattedNode.IsNotNull)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNuint != formattedNode.IsNuint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsUnmanaged != formattedNode.IsUnmanaged)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsVar != formattedNode.IsVar)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult ComparePrefixUnaryExpressionSyntax(PrefixUnaryExpressionSyntax originalNode, PrefixUnaryExpressionSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Operand, originalNode));
        formattedStack.Push((formattedNode.Operand, formattedNode));
        result = Compare(originalNode.OperatorToken, formattedNode.OperatorToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult ComparePrimaryConstructorBaseTypeSyntax(PrimaryConstructorBaseTypeSyntax originalNode, PrimaryConstructorBaseTypeSyntax formattedNode)
    {
        originalStack.Push((originalNode.ArgumentList, originalNode));
        formattedStack.Push((formattedNode.ArgumentList, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult ComparePropertyDeclarationSyntax(PropertyDeclarationSyntax originalNode, PropertyDeclarationSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.AccessorList, originalNode));
        formattedStack.Push((formattedNode.AccessorList, formattedNode));
        result = CompareLists(originalNode.AttributeLists, formattedNode.AttributeLists, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ExplicitInterfaceSpecifier, originalNode));
        formattedStack.Push((formattedNode.ExplicitInterfaceSpecifier, formattedNode));
        originalStack.Push((originalNode.ExpressionBody, originalNode));
        formattedStack.Push((formattedNode.ExpressionBody, formattedNode));
        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Initializer, originalNode));
        formattedStack.Push((formattedNode.Initializer, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult ComparePropertyPatternClauseSyntax(PropertyPatternClauseSyntax originalNode, PropertyPatternClauseSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseBraceToken, formattedNode.CloseBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBraceToken, formattedNode.OpenBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Subpatterns, formattedNode.Subpatterns, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Subpatterns),
            AllSeparatorsButLast(formattedNode.Subpatterns),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareQualifiedCrefSyntax(QualifiedCrefSyntax originalNode, QualifiedCrefSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Container, originalNode));
        formattedStack.Push((formattedNode.Container, formattedNode));
        result = Compare(originalNode.DotToken, formattedNode.DotToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Member, originalNode));
        formattedStack.Push((formattedNode.Member, formattedNode));
        return default;
    }

    private CompareResult CompareQualifiedNameSyntax(QualifiedNameSyntax originalNode, QualifiedNameSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DotToken, formattedNode.DotToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNint != formattedNode.IsNint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNotNull != formattedNode.IsNotNull)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNuint != formattedNode.IsNuint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsUnmanaged != formattedNode.IsUnmanaged)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsVar != formattedNode.IsVar)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Left, originalNode));
        formattedStack.Push((formattedNode.Left, formattedNode));
        originalStack.Push((originalNode.Right, originalNode));
        formattedStack.Push((formattedNode.Right, formattedNode));
        return default;
    }

    private CompareResult CompareQueryBodySyntax(QueryBodySyntax originalNode, QueryBodySyntax formattedNode)
    {
        CompareResult result = CompareLists(originalNode.Clauses, formattedNode.Clauses, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Continuation, originalNode));
        formattedStack.Push((formattedNode.Continuation, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.SelectOrGroup, originalNode));
        formattedStack.Push((formattedNode.SelectOrGroup, formattedNode));
        return default;
    }

    private CompareResult CompareQueryContinuationSyntax(QueryContinuationSyntax originalNode, QueryContinuationSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Body, originalNode));
        formattedStack.Push((formattedNode.Body, formattedNode));
        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.IntoKeyword, formattedNode.IntoKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareQueryExpressionSyntax(QueryExpressionSyntax originalNode, QueryExpressionSyntax formattedNode)
    {
        originalStack.Push((originalNode.Body, originalNode));
        formattedStack.Push((formattedNode.Body, formattedNode));
        originalStack.Push((originalNode.FromClause, originalNode));
        formattedStack.Push((formattedNode.FromClause, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareRangeExpressionSyntax(RangeExpressionSyntax originalNode, RangeExpressionSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.LeftOperand, originalNode));
        formattedStack.Push((formattedNode.LeftOperand, formattedNode));
        result = Compare(originalNode.OperatorToken, formattedNode.OperatorToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.RightOperand, originalNode));
        formattedStack.Push((formattedNode.RightOperand, formattedNode));
        return default;
    }

    private CompareResult CompareRecordDeclarationSyntax(RecordDeclarationSyntax originalNode, RecordDeclarationSyntax formattedNode)
    {
        CompareResult result;
        if (CompareFullSpan(originalNode, formattedNode))
            return default;

        result = CompareLists(originalNode.AttributeLists, formattedNode.AttributeLists, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.BaseList, originalNode));
        formattedStack.Push((formattedNode.BaseList, formattedNode));
        result = Compare(originalNode.ClassOrStructKeyword, formattedNode.ClassOrStructKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseBraceToken, formattedNode.CloseBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.ConstraintClauses, formattedNode.ConstraintClauses, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Members, formattedNode.Members, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenBraceToken, formattedNode.OpenBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ParameterList, originalNode));
        formattedStack.Push((formattedNode.ParameterList, formattedNode));
        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.TypeParameterList, originalNode));
        formattedStack.Push((formattedNode.TypeParameterList, formattedNode));
        return default;
    }

    private CompareResult CompareRecursivePatternSyntax(RecursivePatternSyntax originalNode, RecursivePatternSyntax formattedNode)
    {
        originalStack.Push((originalNode.Designation, originalNode));
        formattedStack.Push((formattedNode.Designation, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.PositionalPatternClause, originalNode));
        formattedStack.Push((formattedNode.PositionalPatternClause, formattedNode));
        originalStack.Push((originalNode.PropertyPatternClause, originalNode));
        formattedStack.Push((formattedNode.PropertyPatternClause, formattedNode));
        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareReferenceDirectiveTriviaSyntax(ReferenceDirectiveTriviaSyntax originalNode, ReferenceDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.File, formattedNode.File, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.ReferenceKeyword, formattedNode.ReferenceKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareRefExpressionSyntax(RefExpressionSyntax originalNode, RefExpressionSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.RefKeyword, formattedNode.RefKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareRefStructConstraintSyntax(RefStructConstraintSyntax originalNode, RefStructConstraintSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.RefKeyword, formattedNode.RefKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.StructKeyword, formattedNode.StructKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareRefTypeExpressionSyntax(RefTypeExpressionSyntax originalNode, RefTypeExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareRefTypeSyntax(RefTypeSyntax originalNode, RefTypeSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNint != formattedNode.IsNint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNotNull != formattedNode.IsNotNull)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNuint != formattedNode.IsNuint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsUnmanaged != formattedNode.IsUnmanaged)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsVar != formattedNode.IsVar)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.ReadOnlyKeyword, formattedNode.ReadOnlyKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.RefKeyword, formattedNode.RefKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareRefValueExpressionSyntax(RefValueExpressionSyntax originalNode, RefValueExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Comma, formattedNode.Comma, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareRegionDirectiveTriviaSyntax(RegionDirectiveTriviaSyntax originalNode, RegionDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.RegionKeyword, formattedNode.RegionKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareRelationalPatternSyntax(RelationalPatternSyntax originalNode, RelationalPatternSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OperatorToken, formattedNode.OperatorToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareReturnStatementSyntax(ReturnStatementSyntax originalNode, ReturnStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.ReturnKeyword, formattedNode.ReturnKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareScopedTypeSyntax(ScopedTypeSyntax originalNode, ScopedTypeSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNint != formattedNode.IsNint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNotNull != formattedNode.IsNotNull)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNuint != formattedNode.IsNuint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsUnmanaged != formattedNode.IsUnmanaged)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsVar != formattedNode.IsVar)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.ScopedKeyword, formattedNode.ScopedKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareSelectClauseSyntax(SelectClauseSyntax originalNode, SelectClauseSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.SelectKeyword, formattedNode.SelectKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareShebangDirectiveTriviaSyntax(ShebangDirectiveTriviaSyntax originalNode, ShebangDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.ExclamationToken, formattedNode.ExclamationToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareSimpleBaseTypeSyntax(SimpleBaseTypeSyntax originalNode, SimpleBaseTypeSyntax formattedNode)
    {
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareSimpleLambdaExpressionSyntax(SimpleLambdaExpressionSyntax originalNode, SimpleLambdaExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ArrowToken, formattedNode.ArrowToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.AsyncKeyword, formattedNode.AsyncKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.AttributeLists, formattedNode.AttributeLists, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Block, originalNode));
        formattedStack.Push((formattedNode.Block, formattedNode));
        originalStack.Push((originalNode.Body, originalNode));
        formattedStack.Push((formattedNode.Body, formattedNode));
        originalStack.Push((originalNode.ExpressionBody, originalNode));
        formattedStack.Push((formattedNode.ExpressionBody, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Parameter, originalNode));
        formattedStack.Push((formattedNode.Parameter, formattedNode));
        return default;
    }

    private CompareResult CompareSingleVariableDesignationSyntax(SingleVariableDesignationSyntax originalNode, SingleVariableDesignationSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareSizeOfExpressionSyntax(SizeOfExpressionSyntax originalNode, SizeOfExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareSkippedTokensTriviaSyntax(SkippedTokensTriviaSyntax originalNode, SkippedTokensTriviaSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(originalNode.Tokens, formattedNode.Tokens, CompareFunc, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareSlicePatternSyntax(SlicePatternSyntax originalNode, SlicePatternSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DotDotToken, formattedNode.DotDotToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Pattern, originalNode));
        formattedStack.Push((formattedNode.Pattern, formattedNode));
        return default;
    }

    private CompareResult CompareSpreadElementSyntax(SpreadElementSyntax originalNode, SpreadElementSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OperatorToken, formattedNode.OperatorToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareStackAllocArrayCreationExpressionSyntax(
        StackAllocArrayCreationExpressionSyntax originalNode,
        StackAllocArrayCreationExpressionSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Initializer, originalNode));
        formattedStack.Push((formattedNode.Initializer, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.StackAllocKeyword, formattedNode.StackAllocKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareStructDeclarationSyntax(StructDeclarationSyntax originalNode, StructDeclarationSyntax formattedNode)
    {
        CompareResult result;
        if (CompareFullSpan(originalNode, formattedNode))
            return default;

        result = CompareLists(originalNode.AttributeLists, formattedNode.AttributeLists, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.BaseList, originalNode));
        formattedStack.Push((formattedNode.BaseList, formattedNode));
        result = Compare(originalNode.CloseBraceToken, formattedNode.CloseBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.ConstraintClauses, formattedNode.ConstraintClauses, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Members, formattedNode.Members, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            originalNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            formattedNode.Modifiers.OrderBy(o => o.Text).ToArray(),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenBraceToken, formattedNode.OpenBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.ParameterList, originalNode));
        formattedStack.Push((formattedNode.ParameterList, formattedNode));
        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.TypeParameterList, originalNode));
        formattedStack.Push((formattedNode.TypeParameterList, formattedNode));
        return default;
    }

    private CompareResult CompareSubpatternSyntax(SubpatternSyntax originalNode, SubpatternSyntax formattedNode)
    {
        originalStack.Push((originalNode.ExpressionColon, originalNode));
        formattedStack.Push((formattedNode.ExpressionColon, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.NameColon, originalNode));
        formattedStack.Push((formattedNode.NameColon, formattedNode));
        originalStack.Push((originalNode.Pattern, originalNode));
        formattedStack.Push((formattedNode.Pattern, formattedNode));
        return default;
    }

    private CompareResult CompareSwitchExpressionArmSyntax(SwitchExpressionArmSyntax originalNode, SwitchExpressionArmSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.EqualsGreaterThanToken, formattedNode.EqualsGreaterThanToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Pattern, originalNode));
        formattedStack.Push((formattedNode.Pattern, formattedNode));
        originalStack.Push((originalNode.WhenClause, originalNode));
        formattedStack.Push((formattedNode.WhenClause, formattedNode));
        return default;
    }

    private CompareResult CompareSwitchExpressionSyntax(SwitchExpressionSyntax originalNode, SwitchExpressionSyntax formattedNode)
    {
        CompareResult result = CompareLists(originalNode.Arms, formattedNode.Arms, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(AllSeparatorsButLast(originalNode.Arms), AllSeparatorsButLast(formattedNode.Arms), CompareFunc, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseBraceToken, formattedNode.CloseBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.GoverningExpression, originalNode));
        formattedStack.Push((formattedNode.GoverningExpression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBraceToken, formattedNode.OpenBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SwitchKeyword, formattedNode.SwitchKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareSwitchSectionSyntax(SwitchSectionSyntax originalNode, SwitchSectionSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(originalNode.Labels, formattedNode.Labels, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Statements, formattedNode.Statements, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareSwitchStatementSyntax(SwitchStatementSyntax originalNode, SwitchStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseBraceToken, formattedNode.CloseBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenBraceToken, formattedNode.OpenBraceToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Sections, formattedNode.Sections, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SwitchKeyword, formattedNode.SwitchKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareThisExpressionSyntax(ThisExpressionSyntax originalNode, ThisExpressionSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Token, formattedNode.Token, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareThrowExpressionSyntax(ThrowExpressionSyntax originalNode, ThrowExpressionSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.ThrowKeyword, formattedNode.ThrowKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareThrowStatementSyntax(ThrowStatementSyntax originalNode, ThrowStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.ThrowKeyword, formattedNode.ThrowKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareTryStatementSyntax(TryStatementSyntax originalNode, TryStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Block, originalNode));
        formattedStack.Push((formattedNode.Block, formattedNode));
        result = CompareLists(originalNode.Catches, formattedNode.Catches, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Finally, originalNode));
        formattedStack.Push((formattedNode.Finally, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.TryKeyword, formattedNode.TryKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareTupleElementSyntax(TupleElementSyntax originalNode, TupleElementSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareTupleExpressionSyntax(TupleExpressionSyntax originalNode, TupleExpressionSyntax formattedNode)
    {
        CompareResult result = CompareLists(originalNode.Arguments, formattedNode.Arguments, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Arguments),
            AllSeparatorsButLast(formattedNode.Arguments),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareTupleTypeSyntax(TupleTypeSyntax originalNode, TupleTypeSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Elements, formattedNode.Elements, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Elements),
            AllSeparatorsButLast(formattedNode.Elements),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNint != formattedNode.IsNint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNotNull != formattedNode.IsNotNull)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsNuint != formattedNode.IsNuint)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsUnmanaged != formattedNode.IsUnmanaged)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsVar != formattedNode.IsVar)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareTypeArgumentListSyntax(TypeArgumentListSyntax originalNode, TypeArgumentListSyntax formattedNode)
    {
        CompareResult result = CompareLists(originalNode.Arguments, formattedNode.Arguments, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Arguments),
            AllSeparatorsButLast(formattedNode.Arguments),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.GreaterThanToken, formattedNode.GreaterThanToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.LessThanToken, formattedNode.LessThanToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareTypeConstraintSyntax(TypeConstraintSyntax originalNode, TypeConstraintSyntax formattedNode)
    {
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareTypeCrefSyntax(TypeCrefSyntax originalNode, TypeCrefSyntax formattedNode)
    {
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareTypeOfExpressionSyntax(TypeOfExpressionSyntax originalNode, TypeOfExpressionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Keyword, formattedNode.Keyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareTypeParameterConstraintClauseSyntax(TypeParameterConstraintClauseSyntax originalNode, TypeParameterConstraintClauseSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ColonToken, formattedNode.ColonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Constraints, formattedNode.Constraints, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Constraints),
            AllSeparatorsButLast(formattedNode.Constraints),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        result = Compare(originalNode.WhereKeyword, formattedNode.WhereKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareTypeParameterListSyntax(TypeParameterListSyntax originalNode, TypeParameterListSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.GreaterThanToken, formattedNode.GreaterThanToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.LessThanToken, formattedNode.LessThanToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.Parameters, formattedNode.Parameters, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Parameters),
            AllSeparatorsButLast(formattedNode.Parameters),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareTypeParameterSyntax(TypeParameterSyntax originalNode, TypeParameterSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.VarianceKeyword, formattedNode.VarianceKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareTypePatternSyntax(TypePatternSyntax originalNode, TypePatternSyntax formattedNode)
    {
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        return default;
    }

    private CompareResult CompareUnaryPatternSyntax(UnaryPatternSyntax originalNode, UnaryPatternSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OperatorToken, formattedNode.OperatorToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Pattern, originalNode));
        formattedStack.Push((formattedNode.Pattern, formattedNode));
        return default;
    }

    private CompareResult CompareUndefDirectiveTriviaSyntax(UndefDirectiveTriviaSyntax originalNode, UndefDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Name, formattedNode.Name, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.UndefKeyword, formattedNode.UndefKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareUnsafeStatementSyntax(UnsafeStatementSyntax originalNode, UnsafeStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Block, originalNode));
        formattedStack.Push((formattedNode.Block, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.UnsafeKeyword, formattedNode.UnsafeKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareUsingDirectiveSyntax(UsingDirectiveSyntax originalNode, UsingDirectiveSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Alias, originalNode));
        formattedStack.Push((formattedNode.Alias, formattedNode));
        result = Compare(originalNode.GlobalKeyword, formattedNode.GlobalKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        originalStack.Push((originalNode.NamespaceOrType, originalNode));
        formattedStack.Push((formattedNode.NamespaceOrType, formattedNode));
        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.StaticKeyword, formattedNode.StaticKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.UnsafeKeyword, formattedNode.UnsafeKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.UsingKeyword, formattedNode.UsingKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareUsingStatementSyntax(UsingStatementSyntax originalNode, UsingStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.AwaitKeyword, formattedNode.AwaitKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Declaration, originalNode));
        formattedStack.Push((formattedNode.Declaration, formattedNode));
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Statement, originalNode));
        formattedStack.Push((formattedNode.Statement, formattedNode));
        result = Compare(originalNode.UsingKeyword, formattedNode.UsingKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareVariableDeclarationSyntax(VariableDeclarationSyntax originalNode, VariableDeclarationSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Type, originalNode));
        formattedStack.Push((formattedNode.Type, formattedNode));
        result = CompareLists(originalNode.Variables, formattedNode.Variables, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = CompareLists(
            AllSeparatorsButLast(originalNode.Variables),
            AllSeparatorsButLast(formattedNode.Variables),
            CompareFunc,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareVariableDeclaratorSyntax(VariableDeclaratorSyntax originalNode, VariableDeclaratorSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.ArgumentList, originalNode));
        formattedStack.Push((formattedNode.ArgumentList, formattedNode));
        result = Compare(originalNode.Identifier, formattedNode.Identifier, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Initializer, originalNode));
        formattedStack.Push((formattedNode.Initializer, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        return default;
    }

    private CompareResult CompareVarPatternSyntax(VarPatternSyntax originalNode, VarPatternSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Designation, originalNode));
        formattedStack.Push((formattedNode.Designation, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.VarKeyword, formattedNode.VarKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareWarningDirectiveTriviaSyntax(WarningDirectiveTriviaSyntax originalNode, WarningDirectiveTriviaSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.DirectiveNameToken, formattedNode.DirectiveNameToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EndOfDirectiveToken, formattedNode.EndOfDirectiveToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.HashToken, formattedNode.HashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsActive != formattedNode.IsActive)
            return NotEqual(originalNode, formattedNode);

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.WarningKeyword, formattedNode.WarningKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareWhenClauseSyntax(WhenClauseSyntax originalNode, WhenClauseSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Condition, originalNode));
        formattedStack.Push((formattedNode.Condition, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.WhenKeyword, formattedNode.WhenKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareWhereClauseSyntax(WhereClauseSyntax originalNode, WhereClauseSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Condition, originalNode));
        formattedStack.Push((formattedNode.Condition, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.WhereKeyword, formattedNode.WhereKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareWhileStatementSyntax(WhileStatementSyntax originalNode, WhileStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.CloseParenToken, formattedNode.CloseParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Condition, originalNode));
        formattedStack.Push((formattedNode.Condition, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.OpenParenToken, formattedNode.OpenParenToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Statement, originalNode));
        formattedStack.Push((formattedNode.Statement, formattedNode));
        result = Compare(originalNode.WhileKeyword, formattedNode.WhileKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareWithExpressionSyntax(WithExpressionSyntax originalNode, WithExpressionSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        originalStack.Push((originalNode.Initializer, originalNode));
        formattedStack.Push((formattedNode.Initializer, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.WithKeyword, formattedNode.WithKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareXmlCDataSectionSyntax(XmlCDataSectionSyntax originalNode, XmlCDataSectionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.EndCDataToken, formattedNode.EndCDataToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.StartCDataToken, formattedNode.StartCDataToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.TextTokens, formattedNode.TextTokens, CompareFunc, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareXmlCommentSyntax(XmlCommentSyntax originalNode, XmlCommentSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.LessThanExclamationMinusMinusToken, formattedNode.LessThanExclamationMinusMinusToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.MinusMinusGreaterThanToken, formattedNode.MinusMinusGreaterThanToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.TextTokens, formattedNode.TextTokens, CompareFunc, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareXmlCrefAttributeSyntax(XmlCrefAttributeSyntax originalNode, XmlCrefAttributeSyntax formattedNode)
    {
        CompareResult result;
        originalStack.Push((originalNode.Cref, originalNode));
        formattedStack.Push((formattedNode.Cref, formattedNode));
        result = Compare(originalNode.EndQuoteToken, formattedNode.EndQuoteToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EqualsToken, formattedNode.EqualsToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        result = Compare(originalNode.StartQuoteToken, formattedNode.StartQuoteToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareXmlElementEndTagSyntax(XmlElementEndTagSyntax originalNode, XmlElementEndTagSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.GreaterThanToken, formattedNode.GreaterThanToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.LessThanSlashToken, formattedNode.LessThanSlashToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        return default;
    }

    private CompareResult CompareXmlElementStartTagSyntax(XmlElementStartTagSyntax originalNode, XmlElementStartTagSyntax formattedNode)
    {
        CompareResult result = CompareLists(originalNode.Attributes, formattedNode.Attributes, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.GreaterThanToken, formattedNode.GreaterThanToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.LessThanToken, formattedNode.LessThanToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        return default;
    }

    private CompareResult CompareXmlElementSyntax(XmlElementSyntax originalNode, XmlElementSyntax formattedNode)
    {
        CompareResult result = CompareLists(originalNode.Content, formattedNode.Content, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.EndTag, originalNode));
        formattedStack.Push((formattedNode.EndTag, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.StartTag, originalNode));
        formattedStack.Push((formattedNode.StartTag, formattedNode));
        return default;
    }

    private CompareResult CompareXmlEmptyElementSyntax(XmlEmptyElementSyntax originalNode, XmlEmptyElementSyntax formattedNode)
    {
        CompareResult result = CompareLists(originalNode.Attributes, formattedNode.Attributes, static (_, _) => default, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.LessThanToken, formattedNode.LessThanToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        result = Compare(originalNode.SlashGreaterThanToken, formattedNode.SlashGreaterThanToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareXmlNameAttributeSyntax(XmlNameAttributeSyntax originalNode, XmlNameAttributeSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.EndQuoteToken, formattedNode.EndQuoteToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EqualsToken, formattedNode.EqualsToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Identifier, originalNode));
        formattedStack.Push((formattedNode.Identifier, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        result = Compare(originalNode.StartQuoteToken, formattedNode.StartQuoteToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareXmlNameSyntax(XmlNameSyntax originalNode, XmlNameSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.LocalName, formattedNode.LocalName, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Prefix, originalNode));
        formattedStack.Push((formattedNode.Prefix, formattedNode));
        return default;
    }

    private CompareResult CompareXmlPrefixSyntax(XmlPrefixSyntax originalNode, XmlPrefixSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.ColonToken, formattedNode.ColonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.Prefix, formattedNode.Prefix, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareXmlProcessingInstructionSyntax(XmlProcessingInstructionSyntax originalNode, XmlProcessingInstructionSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.EndProcessingInstructionToken, formattedNode.EndProcessingInstructionToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        result = Compare(originalNode.StartProcessingInstructionToken, formattedNode.StartProcessingInstructionToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.TextTokens, formattedNode.TextTokens, CompareFunc, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareXmlTextAttributeSyntax(XmlTextAttributeSyntax originalNode, XmlTextAttributeSyntax formattedNode)
    {
        CompareResult result = Compare(originalNode.EndQuoteToken, formattedNode.EndQuoteToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.EqualsToken, formattedNode.EqualsToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        originalStack.Push((originalNode.Name, originalNode));
        formattedStack.Push((formattedNode.Name, formattedNode));
        result = Compare(originalNode.StartQuoteToken, formattedNode.StartQuoteToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = CompareLists(originalNode.TextTokens, formattedNode.TextTokens, CompareFunc, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareXmlTextSyntax(XmlTextSyntax originalNode, XmlTextSyntax formattedNode)
    {
        CompareResult result;
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = CompareLists(originalNode.TextTokens, formattedNode.TextTokens, CompareFunc, o => o.Span, originalNode.Span, formattedNode.Span);
        if (result.IsInvalid)
            return result;

        return default;
    }

    private CompareResult CompareYieldStatementSyntax(YieldStatementSyntax originalNode, YieldStatementSyntax formattedNode)
    {
        CompareResult result = CompareLists(
            originalNode.AttributeLists,
            formattedNode.AttributeLists,
            static (_, _) => default,
            o => o.Span,
            originalNode.Span,
            formattedNode.Span);
        if (result.IsInvalid)
            return result;

        originalStack.Push((originalNode.Expression, originalNode));
        formattedStack.Push((formattedNode.Expression, formattedNode));
        if (originalNode.IsMissing != formattedNode.IsMissing)
            return NotEqual(originalNode, formattedNode);

        result = Compare(originalNode.ReturnOrBreakKeyword, formattedNode.ReturnOrBreakKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.SemicolonToken, formattedNode.SemicolonToken, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        result = Compare(originalNode.YieldKeyword, formattedNode.YieldKeyword, originalNode, formattedNode);
        if (result.IsInvalid)
            return result;

        return default;
    }
}