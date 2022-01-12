import React from "react";
import CodeBlock from '@theme/CodeBlock';
import { requireVersionedCode } from '@theme/utils';

export default ({name}) => {
    const sourceCode = requireVersionedCode(name);
    return <CodeBlock className="language-csharp">{sourceCode}</CodeBlock>;
};