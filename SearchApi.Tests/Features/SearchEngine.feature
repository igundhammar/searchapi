# language: en

Feature: Search Engine

  @ignore
Scenario: Searching for multiple words in multiple engines
    Given the following search words:
      | word     |
      | apple    |
      | banana   |
    And the following engines are available:
      | engineName |
      | Google     |
      | Bing       |
    When I perform a search
    Then I should receive a result from each engine
    And each result should contain total and per-word counts
