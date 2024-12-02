import socket
import cv2
import mediapipe as mp
import json

# Khởi tạo socket
host, port = "127.0.0.1", 65432
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

# Mediapipe Pose
mp_pose = mp.solutions.pose
pose = mp_pose.Pose()

cap = cv2.VideoCapture(0)
padding = 20

while cap.isOpened():
    ret, frame = cap.read()
    if not ret:
        break

    # Xử lý khung hình
    frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
    results = pose.process(frame_rgb)
    frame = cv2.cvtColor(frame_rgb, cv2.COLOR_RGB2BGR)

    if results.pose_landmarks:
        # Lấy bounding box
        # x_min = min([lm.x for lm in results.pose_landmarks.landmark]) * frame.shape[1]
        # x_max = max([lm.x for lm in results.pose_landmarks.landmark]) * frame.shape[1]
        # y_min = min([lm.y for lm in results.pose_landmarks.landmark]) * frame.shape[0]
        # y_max = max([lm.y for lm in results.pose_landmarks.landmark]) * frame.shape[0]

        right_hand_indices = [16, 18, 20, 22]

        right_x = [results.pose_landmarks.landmark[i].x * frame.shape[1] for i in right_hand_indices]
        right_y = [results.pose_landmarks.landmark[i].y * frame.shape[0] for i in right_hand_indices]
        right_x_min, right_x_max = int(min(right_x)), int(max(right_x))
        right_y_min, right_y_max = int(min(right_y)), int(max(right_y))

        right_x_min = max(0, right_x_min - padding)
        right_x_max = min(frame.shape[1], right_x_max + padding)
        right_y_min = max(0, right_y_min - padding)
        right_y_max = min(frame.shape[0], right_y_max + padding)


        # Chuyển về kiểu int để vẽ
        #x_min, y_min, x_max, y_max = map(int, [x_min, y_min, x_max, y_max])

        # Vẽ bounding box
        #cv2.rectangle(frame, (x_min, y_min), (x_max, y_max), (0, 255, 0), 2)
        cv2.rectangle(frame, (right_x_min, right_y_min), (right_x_max, right_y_max), (0, 255, 0), 2)


        # Tạo JSON chứa tọa độ
        data = {
            # "x_min": int(x_min),
            # "y_min": int(y_min),
            # "x_max": int(x_max),
            # "y_max": int(y_max),
            "x_min": right_x_min,
            "y_min": right_y_min,
            "x_max": right_x_max,
            "y_max": right_y_max,
        }

        # Gửi dữ liệu qua socket
        sock.sendto(json.dumps(data).encode(), (host, port))

    # Hiển thị
    cv2.imshow("Bounding Box", frame)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()
