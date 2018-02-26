Feature: ComponentTests

Background: 
Given "5" to do items are created

@Remove
Scenario: Remove Items
	When I remove "1" items
	Then I do not see deleted item in the list any more	
	And I see that items counter displays valid number

@Complete
Scenario: Complete Items
	When I complete "1" items
	Then I see item in the completed state
	And I see that items counter displays valid number

@Complete
Scenario: Complete All Items
	When I complete all items
	Then I see all items in the completed state
	And I see that items counter displays valid number

@Rename
Scenario: Rename Items
    When I rename "2" items
    Then I see that items with new name are displayed                                                                   

		
