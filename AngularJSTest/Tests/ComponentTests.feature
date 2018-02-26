﻿Feature: ComponentTests

Background: 
Given "5" to do items are created

@Remove
Scenario: Remove Items
	When I remove any item
	Then I do not see deleted item in the list any more	

@Complete
Scenario: Complete Items
	When I complete any item
	Then I do not see item in the completed state	
