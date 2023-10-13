// feat_type.h

#ifndef FEAT_TYPE_H
#define FEAT_TYPE_H

#include "eml_fixedpoint.h"

typedef struct
{
    float F1, F2, F3, F4, F5, F6, F7, F8, F9, F10;
} feat_t;

typedef struct 
{
    feat_t feat;
    int32_t highest_idx;
    float distance;
} send_feat_t;

#endif // FEAT_TYPE_H