Feature: E2ETests

Background: 
Given All items are removed
And "10" to do items are created

@end2end
Scenario: Check Filters
	When I complete "4" items
	And I select "Active" filter
	Then I see only active to do items
	When I select "Completed" filter
	Then I see only completed to do items

