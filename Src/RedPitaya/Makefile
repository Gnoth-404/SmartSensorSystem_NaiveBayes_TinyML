CC=gcc
RM=rm
SRC_DIR := src
OBJ_DIR := obj
LIB_DIR := lib
BIN_DIR := .
INC_DIR := inc

SRC := $(wildcard $(SRC_DIR)/*.c)
EXE := $(BIN_DIR)/iic
OBJ := $(SRC:$(SRC_DIR)/%.c=$(OBJ_DIR)/%.o)

CFLAGS		:= -I$(INC_DIR) -MMD -MP -g -std=gnu99 -Wall -Werror -DFIXED_POINT=32 -DDEBUG -I/opt/redpitaya/include/redpitaya
LDFLAGS		:= -L$(LIB_DIR)
LDLIBS		:= -lpthread -lrpxx -lm

all: $(EXE) clean

$(EXE): $(OBJ) | $(BIN_DIR)
	$(CC) $(LDFLAGS) $^ $(LDLIBS) -o $@
$(OBJ_DIR)/%.o: $(SRC_DIR)/%.c | $(OBJ_DIR)
	$(CC) $(CPPFLAGS) $(CFLAGS) -c $< -o $@
$(BIN_DIR) $(OBJ_DIR):
	mkdir -p $@
	

clean:
	$(RM) -r -f $(OBJ_DIR)
#	$(RM) -r -f $(SRC_DIR)
#	$(RM) -r -f $(INC_DIR)
	
	
delete:
	$(RM) -r -f $(OBJ_DIR)
	$(RM) -r $(EXE)
	