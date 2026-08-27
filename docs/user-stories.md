# Workbench — Product Guide & User Stories

> Workbench is general-purpose work management: organize work into **projects**,
> visualize it on a **kanban board**, group it into **milestones**, and track each
> piece of work as an **issue**. This document is the non-technical companion to
> `docs/plan.md` — use it as guidance and reference while implementing.

## What is Workbench?

A team workspace where anyone can spin up a project for whatever the group is
working on — an IT support queue, a software release, a marketing campaign — and
manage it with a board, milestones, and lightweight issue tracking. Think "simple
Jira/Trello hybrid": no admin bureaucracy, projects govern themselves through
their members.

## Glossary (plain language)

| Term          | Meaning                                                                                                                                                                                 |
| ------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Project**   | A container for one body of work (e.g. "Office IT", "Website v2"). Has a **name** and optional description. Whoever creates it becomes its Lead.                                        |
| **Issue**     | One piece of work. Has a **Status** (Open / In Progress / Closed), optional assignee, and optional due date. Referenced anywhere as `#123`.                                             |
| **Board**     | A kanban view of a project's issues as cards in columns. Each project has exactly one board, created automatically with default columns.                                                |
| **Column**    | A stage on the board ("Backlog", "To Do", "In Progress", "Done"). Each has a color and optional WIP limit.                                                                              |
| **Card**      | An issue shown on a board. The card's position and column are visual layout; the issue's Status is the real state. Cards have optional priority.                                        |
| **Milestone** | A dated bucket of issues (e.g. "Launch — Sep 30"). Shows progress (% closed). One issue can belong to several milestones.                                                               |
| **Member**    | Anyone who has joined the workspace via invite code. Members can create issues, comment, vote, and attach files.                                                                        |
| **Lead**      | Manager of a project: manages the board/columns, assigns issues, closes others' work, promotes/demotes members. Every project has at least one; the creator becomes Lead automatically. |
| **Backlink**  | When another issue's comments mention `#your-issue` — you can see everything pointing at your issue.                                                                                    |

## Who can do what

- **Anyone authenticated** sees everything, creates issues in any project,
  comments, votes, attaches files, edits their own things.
- **Project members** can additionally assign **themselves** to issues and
  close/reopen their own issues.
- **Project leads** additionally manage their project: board/columns, assignments
  (assign anyone), closing others' work, managing milestones, promoting members.
- There is no global administrator. New people join with invite codes that any
  member can generate.

---

## User stories

### Joining & identity

- [ ] **US1** — As an invited person, I register with an invite code so I join the workspace as a regular member.
      _Accepts:_ registration requires a valid, unexpired code; code becomes unusable after redemption.
- [ ] **US2** — As any member, I can generate a fresh invite code with an expiry date to onboard a colleague.
- [ ] **US3** — As a visitor, I can only see the login/register pages until authenticated.

### Projects

- [ ] **US10** — As any member, I create a project with a name and description, and automatically become its Lead.
- [ ] **US11** — As a member, I browse all projects and open one to see its overview: issues, board, milestones.
- [ ] **US12** — As a lead of a project, I edit its name/description or delete the project.
- [ ] **US13** — As a lead, I promote other members to lead (and demote them) so management is shared.
- [ ] **US14** — As a member, I see a project's member list and their roles (Lead/Member).

### Issues

- [ ] **US20** — As any member, I create an issue in a project with title, description, and optional assignee/due date.
- [ ] **US21** — As a member, I write `#24` in a comment to reference issue 24; the link is stored by the system.
- [ ] **US22** — As any reader of an issue, I see two lists: "References" (issues it points to) and "Referenced by" (issues pointing here).
- [ ] **US23** — As the author of an issue (or a project member), I can close or reopen it.
- [ ] **US24** — As a lead, I can edit/close/delete any issue in my project, assign any member, and set due date.
- [ ] **US25** — As a member, I comment on issues and attach files; comments may include one image (jpg/png/webp).
- [ ] **US26** — As a member, I up/down-vote issues to signal demand; clicking my vote again removes it.
- [ ] **US27** — As a reader, I see an issue's activity timeline (status changes + comments) and its tags.
- [ ] **US28** — As a member, I can assign myself to an unassigned issue. As a lead, I can assign any member.

### Board

- [ ] **US30** — As a lead, I can customize the board's columns: add, edit (name/description/color/limit), reorder, or remove them.
- [ ] **US31** — As a lead, I add an issue to the board by placing its card in a column.
- [ ] **US32** — As a member, I drag cards between columns / reorder them within a column. WIP limits are enforced.
- [ ] **US33** — As a viewer, opening the board shows all cards grouped by column, ordered, with priority/assignee badges.
- [ ] **US34** — Closing an issue keeps its card visible on the board; status change appears on the timeline.

### Milestones

- [ ] **US40** — As a lead, I create milestones with a name and due date for my project.
- [ ] **US41** — As a lead, I add/remove issues to/from milestones (an issue can be in many milestones).
- [ ] **US42** — As anyone, I see each milestone's progress bar (% of included issues closed) and its due date.

### Reference workflows (end-to-end sanity checks)

1. **IT request flow** — Sam registers via code → opens project "IT" → creates
   issue "VPN drops hourly" → IT lead moves the card to "In Progress", assigns
   Sam's teammate → teammate closes the issue → status change appears on the timeline.
2. **Cross-linking** — While investigating, someone comments "#5 might be related"
   on issue #8; issue #8 now shows #5 under References, and issue #5 lists #8 under
   Referenced by.
3. **Release tracking** — Lead of "Web" creates milestone "v2 Launch" and adds six
   issues; progress shows 2/6 closed; moving cards on the board doesn't change
   milestone membership.
