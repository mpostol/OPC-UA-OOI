# BuildError.Identifier syntax

## ABNF

``` TXT

Identyfier = Part - Section Number
Part = Prefix 2DIGIT
Prefix = "P"
Section = 8DIGIT
Number = 2DIGIT

```

where:

| Rule    | Meaning            | Syntax                                               |
| ------- | ------------------ | ---------------------------------------------------- |
| Prefix  |                    | matches the character `P` literally (case sensitive) |
| Part    | OPC UA part number | matches a digit (equal to [0-9]) exactly 2 times     |
| Section | section number     | matches a digit (equal to [0-9]) exactly 8 times     |
| Number  | sequential number  | matches a digit (equal to [0-9]) exactly 2 times     |

## Reqular expresion

`(P\d{2,2})-(\d{8,8})(\d{2,2})`

where:

- `Part`: OPC UA part number; 1st Capturing Group (P\d{2,2})
  - `P` matches the character `P` literally (case sensitive)
  - `\d{2,2}` matches a digit (equal to [0-9])
    - {2,2} Quantifier — Matches exactly 2 times
- `-` matches the character `-` literally (case sensitive)
- `Section`: section number; 2nd Capturing Group (\d{8,8})
  - `\d{8,8}` matches a digit (equal to [0-9])
    - {8,8} Quantifier — Matches exactly 8 times
- `Number`: sequential number; 3rd Capturing Group (\d{2,2})
  - \d{2,2} matches a digit (equal to [0-9])
    - {2,2} Quantifier — Matches exactly 2 times

## Example

- [P3-0503030201](https://reference.opcfoundation.org/v104/Core/docs/Part3/#5.3.3.2)

## See also

- [Augmented BNF for Syntax Specifications: ABNF; RFC5234; 2008](https://tools.ietf.org/html/rfc5234)
- [Regular expression; From Wikipedia, the free encyclopedia](https://en.wikipedia.org/wiki/Regular_expression)