from flask import Flask, request, jsonify
from datetime import datetime

app = Flask(__name__)

@app.route('/api/reaction/events', methods=['POST'])
def get_events_list():
    data = request.json
    print(data)
    return jsonify({"message": "Data processed successfully", "timestamp": datetime.now()}), 200

@app.route('/api/reaction/summary', methods=['POST'])
def get_summary():
    data = request.json
    print(data)
    return jsonify({"message":"Data processed successfully","timestamp":datetime.now()}),200

if __name__ == "__main__":
    app.run(host="0.0.0.0",port=5001,debug=True)