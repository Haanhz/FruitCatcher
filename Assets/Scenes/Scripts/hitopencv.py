import cv2
import mediapipe as mp
import socket
import json

# Mediapipe
mp_hands = mp.solutions.hands
hands = mp_hands.Hands(min_detection_confidence=0.7, min_tracking_confidence=0.7)

# Socket
host, port = "127.0.0.1", 65433  # Port khác cho người phá
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

# Camera
cap = cv2.VideoCapture(0)

def is_hand_open(landmarks):
    """
    Xác định tay mở hay nắm dựa vào vị trí các điểm landmark.
    Trả về True nếu tay mở, False nếu nắm.
    """
    tips = [8, 12, 16, 20]  # Landmark ngón tay: index, middle, ring, pinky
    base = [6, 10, 14, 18]  # Khớp gần lòng bàn tay

    for tip, base_point in zip(tips, base):
        # Nếu ngón tay cách xa lòng bàn tay, tay đang mở
        if landmarks[tip].y < landmarks[base_point].y:  # Mediapipe: Y nhỏ hơn là ở trên
            return True
    return False

while cap.isOpened():
    ret, frame = cap.read()
    if not ret:
        break

    # Chuyển frame sang RGB
    frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
    results = hands.process(frame_rgb)
    frame = cv2.cvtColor(frame_rgb, cv2.COLOR_RGB2BGR)

    if results.multi_hand_landmarks:
        for hand_landmarks in results.multi_hand_landmarks:
            # Lấy bounding box cho tay trái
            x_coords = [lm.x * frame.shape[1] for lm in hand_landmarks.landmark]
            y_coords = [lm.y * frame.shape[0] for lm in hand_landmarks.landmark]
            x_min, x_max = int(min(x_coords)), int(max(x_coords))
            y_min, y_max = int(min(y_coords)), int(max(y_coords))

            # Kiểm tra trạng thái tay
            hand_open = is_hand_open(hand_landmarks.landmark)

            # Vẽ bounding box
            color = (0, 255, 0) if hand_open else (0, 0, 255)  # Xanh nếu mở, đỏ nếu nắm
            cv2.rectangle(frame, (x_min, y_min), (x_max, y_max), color, 2)

            # Gửi dữ liệu qua Unity
            data = {
                "x_min": x_min,
                "y_min": y_min,
                "x_max": x_max,
                "y_max": y_max,
                "hand_open": hand_open
            }
            sock.sendto(json.dumps(data).encode(), (host, port))

    # Hiển thị
    cv2.imshow("Left Hand Detection", frame)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()
