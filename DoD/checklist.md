## **Definition of Done**

### **General**

- [ ] ESLint reports no errors;
- [ ] Code is written in English (Dutch may be used for formal terms if necessary);
- [ ] Code includes comments where necessary;
- [ ] CamelCasing is used, starting with a lowercase letter. This applies to file names as well. Only classes start with an uppercase letter;
- [ ] Error handling is implemented based on error helpers, as described in the wiki: [Try catch error handling](https://gitlab.com/teamlynn/tasker/wikis/Architectuur/Error-handling);
- [ ] **No console.log statements** are present, only centralized [logging](https://gitlab.com/teamlynn/tasker/wikis/Architectuur/Logging);
- [ ] Code is efficient: duplicate code is minimized.

### **Testing**

Refer to the [Cypress best practices](https://docs.cypress.io/guides/references/best-practices) for more information on the criteria below and how to ensure them.

#### **General**

- [ ] Tests are written according to [Cypress conventions](https://docs.cypress.io/guides/references/best-practices);
- [ ] Specs (`[name].cy.ts`) are created per page and cover all functionality of that page;
- [ ] Tests are '[flake-free](https://www.jetbrains.com/teamcity/ci-cd-guide/concepts/flaky-tests/)'. Do not run the tests only once locally before committing, but multiple times;
- [ ] No static text is tested (hardcoded text that does not change, such as a dialog title); tests are limited to dynamic content;
- [ ] Commands are only created for repetitive code across multiple specs. If code is only used within a single spec, a regular function can be used;
- [ ] All tests pass in the pipeline;
- [ ] Test performance is optimal. No unnecessary [waits](https://docs.cypress.io/api/commands/wait) or (slow) UI actions that can be done programmatically;
- [ ] Actions are only performed via the UI when testing that specific action. Otherwise, they should be done programmatically;
- [ ] URLs are tested for pathname and response code (a correct URL does not mean the page loaded properly). A custom command is available for this;
- [ ] Use [cy.context](https://docs.cypress.io/guides/core-concepts/writing-and-organizing-tests#Test%20Structure) to categorize tests.

#### **Fixtures**

- [ ] Existing fixtures are not modified if they are used by other tests;
- [ ] Fixtures have descriptive names, e.g., `projectWithStudentsAndCollaborators`;
- [ ] After creating new fixtures, the config is updated (`config.ts` & `config.json`).

#### **Permissions**

- [ ] Each functionality is tested with all relevant permissions. Permissions are listed in `_permissions.json_`. For example, CRUD functionality is tested for the owner and collaborators with different permissions.

#### **Data-cy**

- [ ] Only `data-cy` attributes are used to target HTML elements;
- [ ] `Data-cy` attributes use kebab-case and no capital letters;
- [ ] Entity-specific parameters (such as ID or name) are added at the top level (e.g., to a wrapping `<div>` or `<tr>`);
- [ ] `Data-cy` attributes with entity-specific parameters follow this format: `[parameter]-[description(optional)]`, e.g., `"reversi-row"`;
- [ ] Child elements have generic names, such as `"edit-project"` or `"delete-episode"`;
- [ ] Query elements only with `data-cy`, avoid including the element type, e.g., use `[data-cy="new-project"]` instead of `**a**[data-cy="new-project"]`.
