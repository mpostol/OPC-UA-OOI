set COMPILER=OOI.ModelCompilerUI

"%1\%COMPILER%" -d2 "ReferenceTest.xml" -cg "ReferenceTest.csv" -o "ReferenceTest" -console
"%1\%COMPILER%" -d2 "ObjectTypeTest.xml" -cg "ObjectTypeTest.csv" -o "ObjectTypeTest" -console
"%1\%COMPILER%" -d2 "VariableTypeTest.xml" -cg "VariableTypeTest.csv" -o "VariableTypeTest" -console
"%1\%COMPILER%" -d2 "DataTypeTest.xml" -cg "DataTypeTest.csv" -o "DataTypeTest" -console
