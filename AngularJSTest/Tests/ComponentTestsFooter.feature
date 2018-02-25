Feature: ComponentTestsFooter
	
@footer
Scenario Outline: CheckFooterLinks
	When I click on the footer <link>
	Then I see the <page> is opened

Examples:
| link               | page                    |
| Christoph Burgdorf | twitter.com/cburgdorf   |
| Eric Bidelman      | ericbidelman.tumblr.com |
| Jacob Mumm         | jacobmumm.com           |
| Igor Minar         | blog.igorminar.com      |
| TodoMVC            | todomvc.com             |


