# Workbench — API Endpoints

> Nested REST routes. All endpoints require authentication unless noted.
> Error responses use `ProblemDetails` (404, 403, 409, etc.).
> DTOs are in [dtos.md](dtos.md).

---

## Authentication

| Method | Route               | Description                    |
| ------ | ------------------- | ------------------------------ |
| `POST` | `api/auth/register` | Register (form POST, redirect) |
| `POST` | `api/auth/login`    | Login (form POST, redirect)    |
| `POST` | `api/auth/logout`   | Logout (clear cookie)          |

---

## Projects

| Method   | Route                      | Description                     | Auth              |
| -------- | -------------------------- | ------------------------------- | ----------------- |
| `GET`    | `api/projects/mine`        | Current user's projects         | Any authenticated |
| `GET`    | `api/projects`             | List all projects               | Any authenticated |
| `GET`    | `api/projects/{projectId}` | Get project                     | Any authenticated |
| `POST`   | `api/projects`             | Create project (creator = Lead) | Any authenticated |
| `PUT`    | `api/projects/{projectId}` | Update project                  | Lead              |
| `DELETE` | `api/projects/{projectId}` | Delete project                  | Creator only      |

## Project Members

| Method   | Route                                            | Description   | Auth              |
| -------- | ------------------------------------------------ | ------------- | ----------------- |
| `GET`    | `api/projects/{projectId}/members`               | List members  | Any authenticated |
| `POST`   | `api/projects/{projectId}/members`               | Add member    | Lead              |
| `PATCH`  | `api/projects/{projectId}/members/{userId}/role` | Change role   | Lead              |
| `DELETE` | `api/projects/{projectId}/members/{userId}`      | Remove member | Lead              |

---

## Issues (project-scoped)

| Method   | Route                                       | Description           | Auth              |
| -------- | ------------------------------------------- | --------------------- | ----------------- |
| `GET`    | `api/projects/{projectId}/issues`           | List issues           | Any authenticated |
| `GET`    | `api/projects/{projectId}/issues/mine`      | Current user's issues | Any authenticated |
| `GET`    | `api/projects/{projectId}/issues/{issueId}` | Get issue             | Any authenticated |
| `POST`   | `api/projects/{projectId}/issues`           | Create issue          | Any authenticated |
| `PUT`    | `api/projects/{projectId}/issues/{issueId}` | Update issue          | Author or Lead    |
| `DELETE` | `api/projects/{projectId}/issues/{issueId}` | Delete issue          | Author or Lead    |

### Issue sub-resources

All routes below nest under `api/projects/{projectId}/issues/{issueId}/`.

| Method   | Route suffix                 | Description           | Auth              |
| -------- | ---------------------------- | --------------------- | ----------------- |
| `GET`    | `status`                     | Status change history | Any authenticated |
| `PATCH`  | `status`                     | Update status         | Member            |
| `PUT`    | `tags`                       | Replace tags          | Member            |
| `POST`   | `assignments`                | Self-assign           | Member            |
| `DELETE` | `assignments`                | Unassign self         | Member            |
| `POST`   | `attachments`                | Upload attachment     | Member            |
| `DELETE` | `attachments/{attachmentId}` | Delete attachment     | Author            |

### Issue voting

All routes below nest under `api/projects/{projectId}/issues/{issueId}/`.

| Method   | Route suffix | Description             | Auth              |
| -------- | ------------ | ----------------------- | ----------------- |
| `GET`    | `votes/mine` | Get current user's vote | Any authenticated |
| `POST`   | `votes`      | Upvote/downvote/change  | Any authenticated |
| `DELETE` | `votes/mine` | Remove vote             | Any authenticated |

### Comments

All routes below nest under `api/projects/{projectId}/issues/{issueId}/`.

| Method   | Route suffix           | Description    | Auth              |
| -------- | ---------------------- | -------------- | ----------------- |
| `GET`    | `comments`             | List comments  | Any authenticated |
| `POST`   | `comments`             | Add comment    | Any authenticated |
| `PUT`    | `comments/{commentId}` | Update comment | Author            |
| `DELETE` | `comments/{commentId}` | Delete comment | Author            |

### Comment Attachments

| Method   | Route                                                 | Description  | Auth   |
| -------- | ----------------------------------------------------- | ------------ | ------ |
| `POST`   | `api/comments/{commentId}/attachments`                | Upload image | Author |
| `DELETE` | `api/comments/{commentId}/attachments/{attachmentId}` | Delete image | Author |

---

## Kanban (project-scoped)

| Method   | Route                                                | Description                    | Auth              |
| -------- | ---------------------------------------------------- | ------------------------------ | ----------------- |
| `GET`    | `api/projects/{projectId}/kanban`                    | Get board with columns + cards | Any authenticated |
| `POST`   | `api/projects/{projectId}/kanban/columns`            | Add column                     | Lead              |
| `PUT`    | `api/projects/{projectId}/kanban/columns/{columnId}` | Update column                  | Lead              |
| `DELETE` | `api/projects/{projectId}/kanban/columns/{columnId}` | Delete column                  | Lead              |
| `PATCH`  | `api/projects/{projectId}/kanban/columns/reorder`    | Reorder columns                | Lead              |
| `POST`   | `api/projects/{projectId}/kanban/cards`              | Add card                       | Lead              |
| `PUT`    | `api/projects/{projectId}/kanban/cards/{cardId}`     | Move card                      | Lead              |
| `DELETE` | `api/projects/{projectId}/kanban/cards/{cardId}`     | Remove card                    | Lead              |

---

## Milestones (project-scoped)

| Method   | Route                                                      | Description              | Auth              |
| -------- | ---------------------------------------------------------- | ------------------------ | ----------------- |
| `GET`    | `api/projects/{projectId}/milestones`                      | List milestones          | Any authenticated |
| `GET`    | `api/projects/{projectId}/milestones/{milestoneId}`        | Get milestone + progress | Any authenticated |
| `POST`   | `api/projects/{projectId}/milestones`                      | Create milestone         | Lead              |
| `PUT`    | `api/projects/{projectId}/milestones/{milestoneId}`        | Update milestone         | Lead              |
| `DELETE` | `api/projects/{projectId}/milestones/{milestoneId}`        | Delete milestone         | Lead              |
| `PUT`    | `api/projects/{projectId}/milestones/{milestoneId}/issues` | Set issues (replace)     | Lead              |

---

## Attachments (download)

| Method | Route                  | Description              |
| ------ | ---------------------- | ------------------------ |
| `GET`  | `api/attachments/{id}` | Download attachment file |

## Users

| Method | Route          | Description      |
| ------ | -------------- | ---------------- |
| `GET`  | `api/users/me` | Get current user |

## Tags

| Method   | Route             | Description | Auth              |
| -------- | ----------------- | ----------- | ----------------- |
| `GET`    | `api/tags`        | List tags   | Any authenticated |
| `POST`   | `api/tags`        | Create tag  | Lead              |
| `PUT`    | `api/tags/{name}` | Update tag  | Lead              |
| `DELETE` | `api/tags/{name}` | Delete tag  | Lead              |

## Invites

| Method | Route         | Description        | Auth              |
| ------ | ------------- | ------------------ | ----------------- |
| `POST` | `api/invites` | Create invite code | Any authenticated |

---
