# Authorization Rules

This document describes who can perform each operation in Workbench.

**Roles:**

- **Owner** — the user who created the project
- **Lead** — a project member with the Lead role
- **Member** — any project member (Lead or Member)
- **Authenticated** — any logged-in user, regardless of project membership

---

## Projects

| Operation      | Who Can Do It          |
| -------------- | ---------------------- |
| Create project | Any authenticated user |
| View project   | Any authenticated user |
| Update project | Owner                  |
| Delete project | Owner                  |

## Memberships

| Operation                  | Who Can Do It          |
| -------------------------- | ---------------------- |
| View project members       | Any authenticated user |
| Add member to project      | Owner                  |
| Remove member from project | Lead                   |
| Leave project              | Any member (not owner) |

## Issues

| Operation                         | Who Can Do It                |
| --------------------------------- | ---------------------------- |
| View issues                       | Any authenticated user       |
| Create issue                      | Any authenticated user       |
| Update issue title/description    | Project member               |
| Delete issue                      | Issue author OR project Lead |
| Change issue status (Open/Closed) | Project member               |

## Issue Assignments

| Operation                   | Who Can Do It                 |
| --------------------------- | ----------------------------- |
| Self-assign to an issue     | Project member                |
| Unassign self from an issue | Assigned user OR project Lead |
| Assign user to an issue     | project Lead                  |
| Unassign user from an issue | project Lead                  |

## Issue Tags

| Operation             | Who Can Do It  |
| --------------------- | -------------- |
| Edit tags on an issue | Project member |

## Issue Attachments

| Operation                    | Who Can Do It                  |
| ---------------------------- | ------------------------------ |
| Add attachment to issue      | Issue author OR project member |
| Delete attachment from issue | Issue author OR project member |

## Comments

| Operation      | Who Can Do It                    |
| -------------- | -------------------------------- |
| View comments  | Any authenticated user           |
| Create comment | Any authenticated user           |
| Edit comment   | Comment author OR project member |
| Delete comment | Comment author OR project member |

## Comment Attachments

| Operation                      | Who Can Do It                    |
| ------------------------------ | -------------------------------- |
| Add attachment to comment      | Comment author OR project member |
| Delete attachment from comment | Comment author OR project member |

## Project Tags

| Operation  | Who Can Do It                   |
| ---------- | ------------------------------- |
| View tags  | Any authenticated user          |
| Create tag | Project owner OR project member |
| Edit tag   | Project owner OR project member |
| Delete tag | Project owner OR project member |

## Votes

| Operation     | Who Can Do It          |
| ------------- | ---------------------- |
| Vote on issue | Any authenticated user |
| Remove vote   | Any authenticated user |

## Milestones

| Operation                   | Who Can Do It          |
| --------------------------- | ---------------------- |
| View milestones             | Any authenticated user |
| Create milestone            | Project Lead           |
| Update milestone            | Project Lead           |
| Delete milestone            | Project Lead           |
| Add issue to milestone      | Project Lead           |
| Remove issue from milestone | Project Lead           |
