Feature: SecurityTests

@security	
Scenario Outline: Check XSS
	When I add new to do item <item>
	Then I shouldn't see alert
	And I see item <item> is created

Examples:
| item																		|
| <script>alert('test1')</script>										    |
| <img src='/img/javascript:alert()' onerror='javascrip:alert().jpg'>       |