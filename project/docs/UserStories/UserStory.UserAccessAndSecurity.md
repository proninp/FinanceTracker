# User Story Регистрация и вход

Как пользователь, я хочу иметь возможность регистрироваться, входить, выходить и управлять своим паролем,
чтобы безопасно работать со своими финансовыми данными и получать к ним доступ с разных устройств.

---

## User Story 1: Регистрация нового пользователя

Как новый пользователь, я хочу зарегистрироваться при помощи e-mail и пароля на сайте, чтобы начать пользоваться приложением.

**Acceptance Criteria:**

- Пользователь вводит e-mail и пароль дважды (для подтверждения).
- Если e-mail уже зарегистрирован — отображается сообщение об ошибке.
- После успешной регистрации:
  - создаётся запись пользователя в системе;
  - отправляется письмо с подтверждением e-mail;
  - пользователь, проходя по ссылке из e-mail, автоматически авторизуется и перенаправляется в личный кабинет (или на страницу создания первой категории/транзакции).
- Пароль должен соответствовать требованиям безопасности (минимум 8 символов, хотя бы одна цифра, одна заглавная буква).
- При неверно введённых данных (короткий пароль, некорректный e-mail) отображаются валидационные ошибки.

**Business Value:**
Обеспечивает возможность начать работу с приложением, формируя индивидуальное хранилище данных для каждого пользователя.

---

## User Story 2: Вход в систему (Sign In)

Как пользователь, я хочу войти в систему с помощью e-mail и пароля, чтобы получить доступ к своим данным и персонализированным настройкам.

**Acceptance Criteria:**

- Пользователь вводит e-mail и пароль.
- Если данные введены корректно:
  - выполняется проверка в базе;
  - создаётся сессия или JWT-токен;
  - пользователь перенаправляется в основное приложение.
- Если e-mail или пароль неверны — отображается сообщение об ошибке.
- Поддерживается опция «Запомнить меня» (с сохранением токена в cookies/localStorage).
- При успешной авторизации приложение загружает персональные данные пользователя (например, категории, транзакции, настройки).

**Business Value:**
Позволяет пользователю безопасно получать доступ к своему финансовому профилю с любого устройства.

---

## User Story 3: Выход из системы (Sign Out)

Как пользователь, я хочу выйти из системы, чтобы завершить сессию и вернуться на страницу входа/регистрации.

**Acceptance Criteria:**

- При нажатии кнопки "Выйти":
  - удаляются токен/сессия;
  - происходит перенаправление на страницу входа (Sign In);
  - локальные данные (кэшированные транзакции, категории и т.п.) очищаются.
- После выхода пользователь не имеет доступа к защищённым страницам без повторной авторизации.

**Business Value:**
Обеспечивает безопасность данных пользователя при работе на общих или чужих устройствах.

---

## User Story 4: Смена пароля (Change Password)

Как пользователь, я хочу иметь возможность изменять свой пароль на сайте, находясь в аутентифицированном состоянии, чтобы повысить безопасность своего аккаунта.

**Acceptance Criteria:**

- Доступно только авторизованным пользователям.
- Пользователь вводит:
  - текущий пароль,
  - новый пароль,
  - подтверждение нового пароля.
- Если текущий пароль указан неверно - отображается ошибка.
- Новый пароль должен соответствовать политикам безопасности.
- После успешной смены пароля пользователь получает уведомление на e-mail.
- Активная сессия завершается и требуется выполнить Sign In с новым паролем.

**Business Value:**
Повышает уровень безопасности учётной записи и снижает риск несанкционированного доступа.

---

## User Story 5: Восстановление пароля (Forgot Password)

Как пользователь, я хочу иметь возможность восстанавливать свой пароль через e-mail сообщение,
находясь в неаутентифицированном состоянии, чтобы восстановить доступ к своему аккаунту, если я забыл пароль.

**Acceptance Criteria:**

- На странице входа доступна ссылка "Забыли пароль?".
- Пользователь вводит свой e-mail.
- Если e-mail зарегистрирован - отправляется письмо со ссылкой на страницу сброса пароля.
- Ссылка для сброса пароля имеет ограниченный срок действия (например, 30 минут).
- Пользователь переходит по ссылке и вводит новый пароль.
- После успешного сброса пароля:
  - старый пароль становится недействительным;
  - пользователю предлагается войти с новым паролем.
- Если e-mail не найден - отображается уведомление без раскрытия факта существования или отсутствия пользователя (для безопасности).

**Business Value:**
Обеспечивает удобный и безопасный способ восстановления доступа без обращения в поддержку.

---

## 💡 Technical / UX Notes

- Авторизация реализуется с помощью **JWT-токенов** (или cookies + refresh token).
- Все пароли хранятся только в зашифрованном виде (bcrypt/argon2).
- Поддерживается возможность "автоматического входа" через сохранённый токен.
- Возможна интеграция с внешними провайдерами OAuth (Google, Apple ID) - как расширение.


```sql
Table user {
  id uuid [pk]
  role int [not null]
  display_name varchar
  first_name varchar
  second_name varchar
  last_name varchar
  is_active boolean [default: true]
  default_time_zone_id uuid [ref: > time_zone.id, null]
  created_at timestamp [not null, default: `now()`]
  created_by uuid [null]
  updated_at timestamp [not null, default: `now()`]
  updated_by uuid [null]
  deleted_at timestamp [null]
  deleted_by uuid [null]
  is_deleted bool [not null]
}

Enum auth_provider_type {
  email
  telegram
  OAuth2
  Google
  github
  Apple
}

Table user_identity {
  id uuid [pk]
  user_id uuid [not null, ref: > user.id]
  provider auth_provider_type [not null] 
  identifier text [not null] // email, telegramId, OAut2 ID etc...
  password_hash text // only used for password-based providers (e.g. 'email')
  is_confirmed boolean [not null, default: false]
  created_at timestamp [not null, default: `now()`]
  updated_at timestamp [not null, default: `now()`]

    indexes {
    (provider, identifier) [unique]
  }
}

Table user_role {
  id uuid [pk]
  user_id uuid [not null, ref: > user.id] 
  role_id uuid [not null, ref: > role.id]
  indexes {
    (user_id)
    (role_id)
  }
}

Table role {
  id uuid [pk]
  name varchar(256) [not null] 
  is_active bool [not null]
  is_default bool [not null]
  claims jsonb [not null]
  created_at timestamp [not null, default: `now()`]
  created_by uuid [null]
  updated_at timestamp [not null, default: `now()`]
  updated_by uuid [null]
  deleted_at timestamp [null]
  deleted_by uuid [null]
  is_deleted bool [not null]
  indexes {
    (name) [unique]
  }
}


Table refresh_token {
  id uuid [pk]
  user_id uuid [not null, ref: > user.id]
  token_hash text [not null]
  expires_at timestamp [not null]
  is_revoked boolean [not null, default: false]
  created_at timestamp [not null, default: `now()`]
  revoked_at timestamp [null]
  last_used_at timestamp [null]
  
  indexes {
    (token_hash) [unique]
    (user_id)
    (expires_at)
  }
}

Table time_zone {
  id uuid [pk]
  iana_name varchar [not null, unique] // 'Europe/Moscow'
  display_name varchar [not null]
}
```

---

**Навигация:**  
[← К списку UserStory](../UserStory.md) | [К следующей UserStory →](./UserStory.Accounts.md)