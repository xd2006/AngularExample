Feature: CorenerCases
	Peculiar scenarios

@Add
Scenario: Add items
When I add items
| item name                                                                                   |
| !123&$                                                                                      |
| verry verrry looongggg onneeeee 123234455555 SSS DDDDD some other words up to desired limit |
|  надо что-то делать                                                                         |
Then I see items are displayed
| item name                                                                                   |
| !123&$                                                                                      |
| verry verrry looongggg onneeeee 123234455555 SSS DDDDD some other words up to desired limit |
| надо что-то делать                                                                          |


@Add
Scenario: Add random items
When I add 15 items                                                                 
Then I see created items are displayed

