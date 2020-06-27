Feature: Example Specification
	As an applicant for Shell
	I would like to verify my solution is correct
	So that I can pass the programming challenge

Background:
	Given the prices are
		| Product | Price |
		| Beans   | 0.65  |
		| Bread   | 0.80  |
		| Milk    | 1.30  |
		| Apples  | 1.00  |
	And the discounts are
		| Product | Discount |
		| Apples  | 10%      |
	And the multibuy offers are
		| If n | If product | Then n | Then product | Discount |
		| 2    | Beans      | 1      | Bread        | 50%      |

Scenario: Example
	When the basket contains
		| Product |
		| Apples  |
		| Milk    |
		| Bread   |
	Then the subtotal should be £3.10
	And the offers should save £0.10
	And the total should be £3.00

Scenario: Empty basket
	When the basket contains
		| Product | Quantity |
	Then the subtotal should be £0.00
	And the offers should save £0.00
	And the total should be £0.00

Scenario: Example with altered casing
	When the basket contains
		| Product |
		| APPLES  |
		| milk    |
		| bREAD   |
	Then the subtotal should be £3.10
	And the offers should save £0.10
	And the total should be £3.00

Scenario: Duplicate line items without quantity
	When the basket contains
		| Product |
		| Apples  |
		| Apples  |
		| Apples  |
		| Milk    |
		| Milk    |
		| Bread   |
	Then the subtotal should be £6.40
	And the offers should save £0.30
	And the total should be £6.10

Scenario: Duplicate line items with quanity
	When the basket contains
		| Product | Quantity |
		| Apples  | 1        |
		| Apples  | 2        |
		| Apples  | 3        |
		| Milk    | 4        |
		| Milk    | 5        |
		| Bread   | 6        |
	Then the subtotal should be £22.50
	And the offers should save £0.60
	And the total should be £21.90

Scenario: One multibuy
	When the basket contains
		| Product | Quantity |
		| Beans   | 2        |
		| Bread   | 2        |
	Then the subtotal should be £2.90
	And the offers should save £0.40
	And the total should be £2.50

Scenario: Two multibuy
	When the basket contains
		| Product | Quantity |
		| Beans   | 4        |
		| Bread   | 3        |
	Then the subtotal should be £5.00
	And the offers should save £0.80
	And the total should be £4.20

Scenario: Unrecognised products Eggs, Spam
	When the basket contains
		| Product |
		| Apples  |
		| Milk    |
		| Bread   |
		| Eggs    |
		| Spam    |
	Then the exception should be of type UnrecognisedProductsException
	And the exception message should be "Eggs, Spam"