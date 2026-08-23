# HelpDesk API Endpoints

## Authentication

| Method | Endpoint           | Description                             |
| ------ | ------------------ | --------------------------------------- |
| POST   | /api/auth/register | Register a new user                     |
| POST   | /api/auth/login    | Login and receive authentication cookie |
| Post   | /api/auth/logout   | Logout and clear authentication cookie  |

## Tickets

| Method | Endpoint                 | Description                                   |
| ------ | ------------------------ | --------------------------------------------- |
| GET    | /api/tickets             | List tickets (filtering, sorting, pagination) |
| POST   | /api/tickets             | Create a new ticket                           |
| GET    | /api/tickets/{id}        | Get ticket by ID                              |
| PUT    | /api/tickets/{id}        | Update ticket                                 |
| PATCH  | /api/tickets/{id}/status | Update ticket status (Open, Closed)           |

### Ticket Comments

| Method | Endpoint                            | Description              |
| ------ | ----------------------------------- | ------------------------ |
| GET    | /api/tickets/{id}/comments          | List comments for ticket |
| POST   | /api/tickets/{id}/comments          | Add comment to ticket    |
| PUT    | /api/tickets/{id}/comments/{commentId} | Update comment content (author or Technician) |

### Comment Attachments

| Method | Endpoint                                    | Description              |
| ------ | ------------------------------------------- | ------------------------ |
| GET    | /api/comments/{commentId}/attachments       | List comment images      |
| POST   | /api/comments/{commentId}/attachments       | Upload image (max 1, jpg/jpeg/png/webp, 5 MB) |
| DELETE | /api/comments/{commentId}/attachments/{attachmentId} | Delete image    |

### Ticket Tags

| Method | Endpoint               | Description                |
| ------ | ---------------------- | -------------------------- |
| PUT    | /api/tickets/{id}/tags | Replace all tags on ticket |

### Ticket Voting

| Method | Endpoint                     | Description                      |
| ------ | ---------------------------- | -------------------------------- |
| GET    | /api/tickets/{id}/votes/mine | Get current user's vote          |
| POST   | /api/tickets/{id}/votes      | Upvote, downvote, or change vote |

### Ticket Attachments

| Method | Endpoint                                     | Description       |
| ------ | -------------------------------------------- | ----------------- |
| POST   | /api/tickets/{id}/attachments                | Upload attachment |
| DELETE | /api/tickets/{id}/attachments/{attachmentId} | Delete attachment |

## Attachments

| Method | Endpoint              | Description              |
| ------ | --------------------- | ------------------------ |
| GET    | /api/attachments/{id} | Download attachment file |

## Tags (IT only)

| Method | Endpoint         | Description |
| ------ | ---------------- | ----------- |
| GET    | /api/tags        | List tags   |
| POST   | /api/tags        | Create tag  |
| PUT    | /api/tags/{name} | Update tag  |

---

## Notes

- All endpoints except register, login, and logout require Cookie-based authentication.
- Tag creation, updating, and deletion are restricted to Technicians.
- Ticket comments are scoped to the ticket they belong to.
- Attachment metadata is embedded in the ticket response DTO.
