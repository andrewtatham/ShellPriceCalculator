Feature: Offer
	A more thorough test of the offers

Background:
	Given the prices are
		| Product | Price |
		| Beans   | 0.65  |
		| Bread   | 0.80  |
		| Milk    | 1.30  |
		| Apples  | 1.00  |
	And the discounts are
		| Product | Discount |
		| Apples  | 33.333%  |
	And the multibuy offers are
		| If n | If product | Then n | Then product | Discount |
		| 3    | Beans      | 2      | Bread        | 75%      |

Scenario: Example but verifying that the discount is rounded
	When the basket contains
		| Product |
		| Apples  |
		| Milk    |
		| Bread   |
	Then the subtotal should be £3.10
	And the offers should save £0.33
	And the total should be £2.77

Scenario: One multibuy
	When the basket contains
		| Product | Quantity |
		| Beans   | 3        |
		| Bread   | 3        |
	Then the subtotal should be £4.35
	And the offers should save £1.20
	And the total should be £3.15

Scenario: Two multibuy
	When the basket contains
		| Product | Quantity |
		| Beans   | 6        |
		| Bread   | 5        |
	Then the subtotal should be £7.90
	And the offers should save £2.40
	And the total should be £5.50

Scenario: Two multibuy with no qualifying products
	When the basket contains
		| Product | Quantity |
		| Beans   | 6        |
	Then the subtotal should be £3.90
	And the offers should save £0.00
	And the total should be £3.90