# Workbench API Endpoints

> Temporary reference — these endpoints exist only while the controllers are kept
> as a test harness. The product UI talks to services directly, not over HTTP.

## Authentication

| Method | Endpoint           | Description                                        |
| ------ | ------------------ | -------------------------------------------------- |
| POST   | /api/auth/register | Register a new user (form POST, redirects on completion) |
| POST   | /api/auth/login    | Login and receive authentication cookie (form POST, redirects to returnUrl) |
| POST   | /api/auth/logout   | Logout and clear authentication cookie             |

## Issues

| Method | Endpoint                | Description                                   |
| ------ | ----------------------- | --------------------------------------------- |
| GET    | /api/issues             | List issues (filtering, sorting, pagination)  |
| POST   | /api/issues             | Create a new issue                            |
| GET    | /api/issues/{id}        | Get issue by ID                               |
| PUT    | /api/issues/{id}        | Update issue                                  |
| DELETE | /api/issues/{id}        | Delete issue                                  |
| PATCH  | /api/issues/{id}/status | Update issue status (Open, Closed)            |
| GET    | /api/issues/{id}/status | Get issue status change history               |

### My Issues

| Method | Endpoint                  | Description                                    |
| ------ | ------------------------- | ---------------------------------------------- |
| GET    | /api/issues/mine          | List issues authored by current user           |
| GET    | /api/issues/mine/assigned | List issues assigned to current user (Technician only) |

### Issue Comments

| Method | Endpoint                              | Description                                    |
| ------ | ------------------------------------- | ---------------------------------------------- |
| GET    | /api/issues/{id}/comments             | List comments for issue                        |
| POST   | /api/issues/{id}/comments             | Add comment to issue                           |
| PUT    | /api/issues/{id}/comments/{commentId} | Update comment content (author or Technician)  |
| DELETE | /api/issues/{id}/comments/{commentId} | Delete comment (author or Technician)          |

### Comment Attachments

Each comment holds at most one image (jpg/jpeg/png/webp, max size from `Attachments:Comments` config).

| Method | Endpoint                                             | Description   |
| ------ | ---------------------------------------------------- | ------------- |
| GET    | /api/comments/{commentId}/attachments                | List comment images |
| POST   | /api/comments/{commentId}/attachments                | Upload image  |
| DELETE | /api/comments/{commentId}/attachments/{attachmentId} | Delete image  |

### Issue Attachments

Limits (max count, size, allowed extensions) come from the `Attachments:Issues` config.

| Method | Endpoint                                           | Description       |
| ------ | -------------------------------------------------- | ----------------- |
| GET    | /api/issues/{id}/attachments                       | List attachments |
| POST   | /api/issues/{id}/attachments                       | Upload attachment |
| DELETE | /api/issues/{id}/attachments/{attachmentId}        | Delete attachment |

### Issue Tags

| Method | Endpoint                | Description                |
| ------ | ----------------------- | -------------------------- |
| PUT    | /api/issues/{id}/tags   | Replace all tags on issue  |

### Issue Assignments

| Method | Endpoint                            | Description                            |
| ------ | ----------------------------------- | -------------------------------------- |
| POST   | /api/issues/{id}/assignments        | Assign current user to issue (Technician only) |
| DELETE | /api/issues/{id}/assignments        | Unassign current user from issue (Technician only) |

### Issue Voting

| Method | Endpoint                      | Description                      |
| ------ | ----------------------------- | -------------------------------- |
| GET    | /api/issues/{id}/votes/mine   | Get current user's vote          |
| POST   | /api/issues/{id}/votes        | Upvote, downvote, or change vote |
| DELETE | /api/issues/{id}/votes/mine   | Remove current user's vote       |

## Attachments

| Method | Endpoint              | Description              |
| ------ | --------------------- | ------------------------ |
| GET    | /api/attachments/{id} | Download attachment file |

## Users

| Method | Endpoint      | Description                          |
| ------ | ------------- | ------------------------------------ |
| GET    | /api/users/me | Get the currently authenticated user |

## Tags (Technician only)

| Method | Endpoint          | Description |
| ------ | ----------------- | ----------- |
| GET    | /api/tags         | List tags   |
| POST   | /api/tags         | Create tag  |
| PUT    | /api/tags/{name}  | Update tag  |
| DELETE | /api/tags/{name}  | Delete tag  |

## Invites (Technician only)

| Method | Endpoint       | Description                                     |
| ------ | -------------- | ----------------------------------------------- |
| POST   | /api/invites   | Create an invite code for technician registration |

---

## Notes

- All endpoints except register and login require Cookie-based authentication.
- Login and register accept form-encoded bodies and respond with redirects; they are intended for browser form posts rather than API clients.
- Authorization is resource-centric: most write operations require being the resource owner or a Technician.
- Comment attachment limits (max 1 image, image extensions only, 5 MB by default) and issue attachment limits (max 10, mixed types, 10 MB by default) are configured in `appsettings.json` under `Attachments`.
- Attachment metadata is embedded in the issue and comment response DTOs.
