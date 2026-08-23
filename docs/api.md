# HelpDesk API Endpoints

## Authentication

| Method | Endpoint           | Description                                        |
| ------ | ------------------ | -------------------------------------------------- |
| POST   | /api/auth/register | Register a new user (form POST, redirects on completion) |
| POST   | /api/auth/login    | Login and receive authentication cookie (form POST, redirects to returnUrl) |
| POST   | /api/auth/logout   | Logout and clear authentication cookie             |

## Tickets

| Method | Endpoint                 | Description                                   |
| ------ | ------------------------ | --------------------------------------------- |
| GET    | /api/tickets             | List tickets (filtering, sorting, pagination) |
| POST   | /api/tickets             | Create a new ticket                           |
| GET    | /api/tickets/{id}        | Get ticket by ID                              |
| PUT    | /api/tickets/{id}        | Update ticket                                 |
| DELETE | /api/tickets/{id}        | Delete ticket                                 |
| PATCH  | /api/tickets/{id}/status | Update ticket status (Open, Closed)           |
| GET    | /api/tickets/{id}/status | Get ticket status change history              |

### My Tickets

| Method | Endpoint                 | Description                                    |
| ------ | ------------------------ | ---------------------------------------------- |
| GET    | /api/tickets/mine        | List tickets authored by current user          |
| GET    | /api/tickets/mine/assigned | List tickets assigned to current user (Technician only) |

### Ticket Comments

| Method | Endpoint                               | Description                                    |
| ------ | -------------------------------------- | ---------------------------------------------- |
| GET    | /api/tickets/{id}/comments             | List comments for ticket                       |
| POST   | /api/tickets/{id}/comments             | Add comment to ticket                          |
| PUT    | /api/tickets/{id}/comments/{commentId} | Update comment content (author or Technician)  |
| DELETE | /api/tickets/{id}/comments/{commentId} | Delete comment (author or Technician)          |

### Comment Attachments

Each comment holds at most one image (jpg/jpeg/png/webp, max size from `Attachments:Comments` config).

| Method | Endpoint                                             | Description   |
| ------ | ---------------------------------------------------- | ------------- |
| GET    | /api/comments/{commentId}/attachments                | List comment images |
| POST   | /api/comments/{commentId}/attachments                | Upload image  |
| DELETE | /api/comments/{commentId}/attachments/{attachmentId} | Delete image  |

### Ticket Attachments

Limits (max count, size, allowed extensions) come from the `Attachments:Tickets` config.

| Method | Endpoint                                             | Description       |
| ------ | ---------------------------------------------------- | ----------------- |
| GET    | /api/tickets/{id}/attachments                        | List attachments |
| POST   | /api/tickets/{id}/attachments                        | Upload attachment |
| DELETE | /api/tickets/{id}/attachments/{attachmentId}         | Delete attachment |

### Ticket Tags

| Method | Endpoint               | Description                |
| ------ | ---------------------- | -------------------------- |
| PUT    | /api/tickets/{id}/tags | Replace all tags on ticket |

### Ticket Assignments

| Method | Endpoint                           | Description                            |
| ------ | ---------------------------------- | -------------------------------------- |
| POST   | /api/tickets/{id}/assignments      | Assign current user to ticket (Technician only) |
| DELETE | /api/tickets/{id}/assignments      | Unassign current user from ticket (Technician only) |

### Ticket Voting

| Method | Endpoint                     | Description                      |
| ------ | ---------------------------- | -------------------------------- |
| GET    | /api/tickets/{id}/votes/mine | Get current user's vote          |
| POST   | /api/tickets/{id}/votes      | Upvote, downvote, or change vote |
| DELETE | /api/tickets/{id}/votes/mine | Remove current user's vote       |

## Attachments

| Method | Endpoint              | Description              |
| ------ | --------------------- | ------------------------ |
| GET    | /api/attachments/{id} | Download attachment file |

## Users

| Method | Endpoint        | Description                          |
| ------ | --------------- | ------------------------------------ |
| GET    | /api/users/me   | Get the currently authenticated user |

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
- Comment attachment limits (max 1 image, image extensions only, 5 MB by default) and ticket attachment limits (max 10, mixed types, 10 MB by default) are configured in `appsettings.json` under `Attachments`.
- Attachment metadata is embedded in the ticket and comment response DTOs.
